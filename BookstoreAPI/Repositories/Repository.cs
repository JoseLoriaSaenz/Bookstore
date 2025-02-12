using System.Runtime.CompilerServices;
using BookstoreAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BookstoreAPI.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal BookstoreContext dbContext;
        internal DbSet<TEntity> dbSet;

        /// <summary>
        /// Constructor for the Repository
        /// </summary>
        /// <param name="context">Database context</param>
        public Repository(BookstoreContext context)
        {
            dbContext = context;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Deletes a entity by the id
        /// </summary>
        /// <param name="id">Entity's Id to be deleted</param>
        /// <returns>Nothing</returns>
        public async Task Delete(int id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
                dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Finds entities based on a query
        /// </summary>
        /// <param name="query">query to be executed against the dbset</param>
        /// <returns>IEnumerable of entities found</returns>
        public async Task<IEnumerable<TEntity?>> Find(string query)
        {
            FormattableString formattableString = FormattableStringFactory.Create(query);
            return await dbSet.FromSql(formattableString).ToListAsync();
        }

        /// <summary>
        /// Gets all entities 
        /// </summary>
        /// <returns>IEnumerable of all entities</returns>
        public async Task<IEnumerable<TEntity?>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity's Id to be found</returns>
        public async Task<TEntity?> GetById(int id)
        {
            var found = await dbSet.FindAsync(id);
            return await dbSet.FindAsync(id);
        }


        /// <summary>
        /// Inserts a new entity
        /// </summary>
        /// <param name="entity">Entity's information</param>
        /// <returns>Nothing</returns>
        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);

        }
    }
}
