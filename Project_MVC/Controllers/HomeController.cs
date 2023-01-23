using Project_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Project_MVC.Models;
using System.Diagnostics;

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
            var products = await _context.Products.ToListAsync();

            return View(products);
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
