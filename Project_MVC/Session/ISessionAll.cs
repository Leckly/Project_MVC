namespace Project_MVC.Session
{
    public interface ISessionAll
    {
        ICategory Category { get; set; }
        IProduct Product { get; set; }
        void Save();
    }
}

