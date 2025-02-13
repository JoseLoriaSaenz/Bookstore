using System.Net;
using BookstoreAPI.Models;
using BookstoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly ILogger<BooksController> _logger;
        private UnitOfWork _unitOfWork;

        public BooksController(ILogger<BooksController> logger, BookstoreContext context)
        {
            _logger = logger;
            _unitOfWork = new UnitOfWork(context);
        }

        //GET: /api/v1/Books/GetBooks
        /// <summary>
        /// Gets all books
        /// </summary>
        /// <returns>200 Code with List of books, if nothing found returns 404 Code Not Found</returns>
        [HttpGet("GetBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var result = await _unitOfWork.Books.GetAll();
            if (!result.Any())
                return NoContent();

            return Ok(result);

        }

        //GET: /api/v1/Books/GetBookById/id
        /// <summary>
        /// Gets a Book by id
        /// </summary>
        /// <param name="id">Integer represent Book id</param>
        /// <returns>200 Code with Book info, if not found returns 404 Code Not found</returns>
        [HttpGet("GetBookById")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var result = await _unitOfWork.Books.GetById(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        //GET: /api/v1/Books/SearchBooks
        /// <summary>
        /// Returns a list of books based on the query used
        /// </summary>
        /// <param name="query">string representing the query need to be executed</param>
        /// <returns>200 Code with list of books, if not found 404 Code Not Found, in case of error returns 400 Code Bad Request </returns>
        [HttpGet("SearchBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(string query)
        {
            try
            {
                var result = await _unitOfWork.Books.Find(query);
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        //POST: /api/v1/Books/AddBook
        /// <summary>
        /// Adds a new book 
        /// </summary>
        /// <param name="book">Book json object with the book information</param>
        /// <returns>201 Code Created</returns>
        [HttpPost("AddBook")]
        public async Task<ActionResult> AddBook(Book book)
        {
            try
            {
                await _unitOfWork.Books.Insert(book);
                await _unitOfWork.Commit();

                return Created();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Conflict(ex);
            }
        }

        //GET: /api/v1/Books/DeleteBook/id
        /// <summary>
        /// Deletes a book using the book id
        /// </summary>
        /// <param name="id">Integer representing Book id</param>
        /// <returns>200 Code</returns>
        [HttpDelete("DeleteBook")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                var result = await _unitOfWork.Books.Delete(id);
                await _unitOfWork.Commit();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Conflict(ex);
            }
        }
    }
}
