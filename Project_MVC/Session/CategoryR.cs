using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Project_MVC.Data;
using Project_MVC.Models;
using System.Linq.Expressions;

namespace Project_MVC.Session
{
    public class CategoryR : Session<Category>, ICategory
    {
        private ApplicationDbContext _context;


        public CategoryR(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public void Update(Category category)
        {
            var categoryDB = _context.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (categoryDB != null)
            {
                categoryDB.Name = category.Name;

            }
        }

    }
}
