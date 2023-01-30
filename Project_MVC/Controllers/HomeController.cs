using Project_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Project_MVC.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Project_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
  

        public async Task<IActionResult> Index()
        {
            try
            {
                var newestProducts = await _context.Products.Where(x => x.AddData > DateTime.Now.AddMinutes(-60)).OrderByDescending(x => x.AddData).Take(3).ToListAsync();
                var bestSellers = await _context.Products.Where(x => x.Bestseller >= 1).OrderByDescending(x => x.Bestseller).Take(3).ToListAsync();
                var homeModel = new HomeModel()
                {
                    NewestProducts = newestProducts,
                    BestSellers = bestSellers
                };
                var getInto = new SiteCounter();
                await _context.SiteCounter.AddAsync(getInto);
                await _context.SaveChangesAsync();
                return View(homeModel);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
