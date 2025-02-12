using BookstoreAPI.Models;

namespace BookstoreAPI.Repositories
{
    public class UnitOfWork(BookstoreContext context) : IUnitOfWork
    {
        private BookstoreContext _dbContext = context;

        /// <summary>
        /// Returns the repository Books using the database context
        /// </summary>
        public IRepository<Book> Books
        {
            get
            {
                return new Repository<Book>(_dbContext);
            }
        }
        

        /// <summary>
        /// Commits any repository change into the database context
        /// </summary>
        /// <returns>Nothing</returns>
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
