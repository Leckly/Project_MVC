using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC.Data;
using Project_MVC.Models;

namespace Project_MVC.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCarts
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> Index()
        {
            List<JoinedShoppingCart> joinedShoppingCart = new();
            joinedShoppingCart = await (from s in _context.ShoppingCarts
                        join p in _context.Products
                        on s.ProductId equals p.ProductId
                        select new JoinedShoppingCart
                        {
                            Id = s.Id,
                            ProductId = s.ProductId,
                            Nazwa = p.Name,
                            Ilosc = s.Ilosc,
                            Cena = p.Price
                        }).ToListAsync();
            ViewBag.Total = joinedShoppingCart.Sum(x=> x.Ilosc * x.Cena);
            return View(joinedShoppingCart);
        }

   
        // GET: ShoppingCarts/Details/5
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

    
        public IActionResult plus(int id)
        {
            var cartItem = _context.ShoppingCarts.FirstOrDefault(m => m.Id == id);
            if (cartItem.Ilosc == 1)
            {
                cartItem.Ilosc++;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult minus (int id)
        {
            var cartItem = _context.ShoppingCarts.FirstOrDefault(m => m.Id == id);
            if (cartItem.Ilosc >1)
            {
                cartItem.Ilosc--;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        /*// GET: ShoppingCarts/Create
        [Authorize(Roles = "User , Admin")]
        public IActionResult Create()
        {
            return View();
        }*/

        /*// POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> Create([Bind("Id,ProductId,UserId,Ilosc")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingCart);
        }*/

        // GET: ShoppingCarts/Edit/5
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,UserId,Ilosc")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.Id))
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
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShoppingCarts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoppingCarts'  is null.");
            }
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
          return _context.ShoppingCarts.Any(e => e.Id == id);
        }
    }
}
