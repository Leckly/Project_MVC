using Project_MVC.Models;

namespace Project_MVC.ViewModel
{
    public class CategoryViewModel
    {
        public Category Category { get; set; } = new Category();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
