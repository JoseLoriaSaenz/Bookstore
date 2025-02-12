using BookstoreAPI.Models;

namespace BookstoreAPI.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Book> Books { get; }
        Task Commit();
    }
}
