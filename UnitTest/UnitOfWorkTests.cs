using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreAPI.Models;
using BookstoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    public class UnitOfWorkTests
    {
        #region Properties

        private DbContextOptions<BookstoreContext> dbContextOptions;
        private readonly BookstoreContext context;
        private UnitOfWork unitOfWork;
        private List<Book> books;

        #endregion

        #region Constructor
        public UnitOfWorkTests()
        {
            var dbName = $"AuthorPostsDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<BookstoreContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            context = CreateDBContextInMemory();
        }

        #endregion

        #region Private Methods

        private BookstoreContext CreateDBContextInMemory()
        {
            return new BookstoreContext(dbContextOptions);
        }
        private void PopulateDataAsync(BookstoreContext context)
        {
            foreach (var book in books)
            {
                context.Books.AddAsync(book);
                context.SaveChangesAsync();
            };
        }
        private UnitOfWork CreateUnitOfWork()
        {
            BookstoreContext context = new BookstoreContext(dbContextOptions);
            return new UnitOfWork(context);
        }

        private static List<Book> GetBooksList()
        {

            List<Book> Books = [
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
                                        Title = "Merrick",
                                        Genre = "Fantasy",
                                        ISBN = "978-0394498218",
                                        PublishedDate = DateTime.Now.Date }
            ];

            return Books;
        }

        #endregion

        #region Setup-TearDown

        [SetUp]
        public void Setup()
        {
            books = GetBooksList();
            unitOfWork = CreateUnitOfWork();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            context.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void UnitOfWork_Books_is_not_null()
        {
            //Arrange

            //Act
            var result = unitOfWork.Books;

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

    }
}
