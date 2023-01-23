using Project_MVC.Data;

namespace Project_MVC.Session
{
    public class SessionAllR : ISessionAll

    {
        private ApplicationDbContext _context; 
        public ICategory Category { get;set; }
        public IProduct Product { get; set; }
        
        public SessionAllR (ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryR(context);
            Product = new ProductR(context);

        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
