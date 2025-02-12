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
        public virtual void Delete(int id)
        {
            TEntity? entityToDelete = dbSet.Find(id);
            if (entityToDelete != null) 
                dbSet.Remove(entityToDelete);
        }

        public virtual IEnumerable<TEntity> Find(string query)
        {
            FormattableString formattableString = FormattableStringFactory.Create(query);
            return dbSet.FromSql(formattableString);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public virtual TEntity GetById(int id)
        {    
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);           
        }
    }
}
