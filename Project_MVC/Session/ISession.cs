using System.Linq.Expressions;

namespace Project_MVC.Session
{
    public interface ISession<T> where T : class
    {
        IEnumerable<T>GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);
        T GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange (IEnumerable<T> entity); 
    }
}
