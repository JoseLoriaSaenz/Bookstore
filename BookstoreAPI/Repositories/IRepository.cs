namespace BookstoreAPI.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all records from the class
        /// </summary>
        /// <returns>Gets a IEnumerable of TEntity</returns>
        Task<IEnumerable<TEntity?>> GetAll();

        /// <summary>
        /// Gets TEntity by Id
        /// </summary>
        /// <param name="id">Id for the TEntity</param>
        /// <returns>Single TEntity</returns>
        Task<TEntity?> GetById(int id);

        /// <summary>
        /// Finds TEntities based on a sql query
        /// </summary>
        /// <param name="query">query to be executed</param>
        /// <returns>Gets a IEnumerable of TEntity</returns>
        Task<IEnumerable<TEntity?>> Find(string query);

        /// <summary>
        /// Insert a new TEntity
        /// </summary>
        /// <param name="entity">TEntity information</param>
        /// <returns>Nothing</returns>
        Task<bool> Insert(TEntity entity);

        /// <summary>
        /// Deletes a TEntity
        /// </summary>
        /// <param name="id">TEntity id to find the TEntity to be deleted</param>
        /// <returns>Nothing</returns>
        Task<bool> Delete(int id);
    }
}
