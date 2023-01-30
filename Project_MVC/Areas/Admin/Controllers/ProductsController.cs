using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC.Models;
using Microsoft.AspNetCore.Authorization;

using Project_MVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_MVC.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index(string order, string search, string currentFilter, int? pageNumber)
        {
            ViewData["Order"] = order;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(order) ? "name_desc" : "";
            ViewData["DescriptionSortParm"] = order == "description" ? "description_desc" : "description";
            ViewData["PriceSortParm"] = order == "price" ? "price_desc" : "price";
            ViewData["BestsellerSortParm"] = order == "bestseller" ? "bestseller_desc" : "bestseller";
            ViewData["ShortDescrprionSortParm"] = order == "shortdescription" ? "shortdescription_desc" : "shortdescription";
            



            var products = await _context.Products.ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            switch (order)
            {
         
                case "description-desc":
                    products = products.OrderByDescending(s => s.Description).ToList();
                    break;
                case "description":
                    products = products.OrderBy(s => s.Description).ToList();
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price).ToList();
                    break;
                case "price":
                    products = products.OrderBy(s => s.Price).ToList();
                    break;
                case "bestseller_desc":
                    products = products.OrderByDescending(s => s.Bestseller).ToList();
                    break;
                case "bestseller":
                    products = products.OrderBy(s => s.Bestseller).ToList();
                    break;
                case "shortdescrpition_desc":
                    products = products.OrderByDescending(s => s.ShortDescription).ToList();
                    break;
                case "shortdescrpition":
                    products = products.OrderBy(s => s.ShortDescription).ToList();
                    break;

                case "name_desc":
                    products = products.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    products = products.OrderBy(s => s.Name).ToList();
                    break;
            }
            List<JoinedProducts> joinedProducts = new();
            joinedProducts = (from p in products.Where(x => x.Hidden == false)
                                    join c in _context.Categories
                                    on p.CategoryId equals c.CategoryId
                                    select new JoinedProducts
                                    {
                                        Id = p.ProductId,
                                        NazwaProduktu = p.Name,
                                        Cena = p.Price,
                                        Opis = p.Description,
                                        KrótkiOpis = p.ShortDescription,
                                        Kategoria = c.Name,
                                        Zdjecieproduktu = p.ImageName
                                    }).ToList();
            int pageSize = 5;
            return View(PaginatedList<JoinedProducts>.Create(joinedProducts, pageNumber ?? 1, pageSize));

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name"); 
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Price,Bestseller,ShortDescription,Image, CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Image != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(product.Image.FileName);
                    string extension = Path.GetExtension(product.Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    product.ImageName = fileName;
                    string path = Path.Combine(wwwRootPath + "/images/", fileName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(wwwRootPath + "/images/");
                    }
                    path = Path.Combine(wwwRootPath + "/images/", fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    await product.Image.CopyToAsync(fileStream);
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }



        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Author,Description,Price,Bestseller,ShortDescription")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (product.ImageName != null)
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", product.ImageName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
