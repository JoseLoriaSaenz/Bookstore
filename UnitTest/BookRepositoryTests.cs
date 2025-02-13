using BookstoreAPI.Models;
using BookstoreAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTest
{
    public class BookRepositoryTests
    {
        #region Properties

        private DbContextOptions<BookstoreContext> dbContextOptions;
        private List<Book> Books = [
                                        new() {
                                            Id = 1,
                                            Author = "Ann Rice",
                                            Title = "Interview with the vampire",
                                            Genre = "Fantasy",
                                            ISBN = "978-0394954218",
                                            PublishedDate = DateTime.Now.Date },
                                        new() {
                                            Id = 2,
                                            Author = "Ann Rice",
                                            Title = "Memnoch the devil",
                                            Genre = "Fantasy",
                                            ISBN = "978-03587498218",
                                            PublishedDate = DateTime.Now.Date },
                                        new() {
                                            Id = 3,
                                            Author = "Ann Rice",
                                            Title = "Interview with the vampire",
                                            Genre = "Fantasy",
                                            ISBN = "978-0394498218",
                                            PublishedDate = DateTime.Now.Date }
        ];
        public BookRepositoryTests()
        {
            var dbName = $"AuthorPostsDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<BookstoreContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }
        public Repository<Book> repository;

        #endregion

        #region Private Methods
        private Repository<Book> CreateBooksRepositoryAsync()
        {
            BookstoreContext context = new BookstoreContext(dbContextOptions);
            PopulateDataAsync(context);
            return new Repository<Book>(context);
        }

        private void PopulateDataAsync(BookstoreContext context)
        {
            foreach (var book in Books)
            {
                context.Books.AddAsync(book);
                context.SaveChangesAsync();
            };
        }

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            repository = CreateBooksRepositoryAsync();
        }

        #endregion

        #region Tests

        [Test]
        public async Task BookRepository_GetBooks_Success()
        {
            //Assert
           // var repository = await CreateBooksRepositoryAsync();

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.That(Books.Count, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task BookRepository_GetBookByIdAsync_Success()
        {
            //Assert
            string authorName = "Ann Rice";

            // Act
            var book = await repository.GetById(2);

            // Assert
            Assert.NotNull(book);
            Assert.That(authorName, Is.EqualTo(book.Author));
        }

        [Test]
        public async Task BookRepository_Add_Success()
        {
            //Assert
            //var repository = await CreateBooksRepositoryAsync();
            Book newBook = new()
            {
                Id = 4,
                Author = "Ann Rice",
                Title = "Merrick",
                Genre = "Fantasy",
                ISBN = "978-0394232218",
                PublishedDate = new DateTime(1998, 07, 14)
            };

            // Act
            var result = await repository.Insert(newBook);
            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task BookRepository_Delete_Success()
        {
            //Assert
           // var repository = await CreateBooksRepositoryAsync();

            // Act
            var result = await repository.Delete(2);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }
        #endregion[Test]
    }
}