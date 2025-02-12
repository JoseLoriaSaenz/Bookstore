using BookstoreAPI.Models;

namespace BookstoreAPI.Repositories
{
    public class UnitOfWork(BookstoreContext context) : IUnitOfWork
    {
        private BookstoreContext _dbContext = context;

        public IRepository<Book> Books
        {
            get
            {
                return new Repository<Book>(_dbContext);
            }
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
