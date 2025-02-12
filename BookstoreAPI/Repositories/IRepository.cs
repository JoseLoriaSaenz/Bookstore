namespace BookstoreAPI.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity?>> GetAll();
        Task<TEntity?> GetById(int id);
        Task<IEnumerable<TEntity?>> Find(string query);
        Task Insert(TEntity entity);
        Task Delete(int id);
    }
}
