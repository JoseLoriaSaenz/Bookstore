using System.Runtime.CompilerServices;
using BookstoreAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BookstoreAPI.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal BookstoreContext dbContext;
        internal DbSet<TEntity> dbSet;

        public Repository(BookstoreContext context)
        {
            dbContext = context;
            dbSet = context.Set<TEntity>();
        }
        public async Task Delete(int id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
                dbSet.Remove(entityToDelete);
        }

        public async Task<IEnumerable<TEntity?>> Find(string query)
        {
            FormattableString formattableString = FormattableStringFactory.Create(query);
            return await dbSet.FromSql(formattableString).ToListAsync();
        }

        public async Task<IEnumerable<TEntity?>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            var found = await dbSet.FindAsync(id);
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);

        }
    }
}
