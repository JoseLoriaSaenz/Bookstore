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

        [HttpGet("GetBooks")]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var result = _unitOfWork.Books.GetAll();
            if (!result.Any())
                return NoContent();

            return Ok(result);

        }

        [HttpGet("GetBookbyId")]
        public ActionResult<Book> GetBookById(int id)
        {
            var result = _unitOfWork.Books.GetById(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("SearchBooks")]
        public ActionResult<IEnumerable<Book>> SearchBooks(string query)
        {
            try
            {
                var result = _unitOfWork.Books.Find(query);
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
         }

        [HttpPost("AddBook")]
        public ActionResult AddBook(Book book)
        {
            try
            {
                _unitOfWork.Books.Insert(book);
                _unitOfWork.Commit();

                return Created();

            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }
        }

        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                _unitOfWork.Books.Delete(id);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }
        }
    }
}
