using Microsoft.EntityFrameworkCore.Update.Internal;
using Project_MVC.Models;

namespace Project_MVC.Session
{
    public interface ICategory : ISession<Category>
    {
        void Update(Category category);
    }
}
