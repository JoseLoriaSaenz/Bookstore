using System.Linq.Expressions;

namespace BookstoreAPI.Repositories
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        IEnumerable<TEntity> Find(string query);   
        void Insert(TEntity entity);
        void Delete(int id);
    }
}
