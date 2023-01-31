using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Project_MVC.Models;
using Project_MVC.Session;
using Project_MVC.ViewModel;
using System.Security.Principal;

namespace Project_MVC.Areas.Admin.Controllers
{
    public class Category : Controller
    {
        private ISessionAll _sessionAll;
        public Category(ISessionAll sessionAll)
        {
            _sessionAll = sessionAll;
        }
        public IActionResult Index()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel(); 
            categoryViewModel.Categories = _sessionAll.Category.GetAll();
            return View(categoryViewModel);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            CategoryViewModel catVM = new CategoryViewModel(); 
            if (id == 0)
            {
                 return View(catVM);
            }
            else
            {
                catVM.Category = _sessionAll.Category.GetT(x => x.CategoryId == id);
                if (catVM.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(catVM);
                } 
                    
            }
          
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel c)
        {
            if (ModelState.IsValid)
            {
                if (c.Category.CategoryId == 0)
                {
                    _sessionAll.Category.Add(c.Category);
                }
                else
                {

                    _sessionAll.Category.Update(c.Category);
                }
                _sessionAll.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id== null || id == 0)
            {
                return NotFound();
            }
            var category = _sessionAll.Category.GetT(x => x.CategoryId == id);
                if (category != null)
                {
                    return NotFound();
                }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {

            var category = _sessionAll.Category.GetT(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            _sessionAll.Category.Delete(category);
            _sessionAll.Save();
            return RedirectToAction("Index");
        }

    }
}
      

