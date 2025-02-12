using BookstoreAPI.Models;

namespace BookstoreAPI.Repositories
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets a Book Repository
        /// </summary>
        IRepository<Book> Books { get; }

        /// <summary>
        /// Commits any changes in the repository
        /// </summary>
        /// <returns>Nothing</returns>
        Task Commit();
    }
}
