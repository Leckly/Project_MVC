using Project_MVC.Models;

namespace Project_MVC.Session
{
    public interface IProduct : ISession<Product>
    {
        void Update(Product product);
    }
}
