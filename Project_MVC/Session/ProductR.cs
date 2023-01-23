using Project_MVC.Data;
using Project_MVC.Models;

namespace Project_MVC.Session
{
    public class ProductR : Session<Product>, IProduct
    {
        private ApplicationDbContext _context;


        public ProductR(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public void Update(Product product)
        {
            var productDB = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (productDB != null)
            {
                productDB.Name = product.Name;
                productDB.Description = product.Description;
                productDB.Price = product.Price;    
                if (product.ImageName != null) 
                {
                    productDB.ImageName = product.ImageName;
                }

            }
        }

    }
}
