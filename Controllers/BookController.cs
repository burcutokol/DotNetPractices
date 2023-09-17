using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext; //sadece contstructorda set edilebilir.
        public BookController(BookStoreDbContext context)
        {
            _dbContext = context;
        }

    
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery q = new GetBooksQuery(_dbContext);
            var res = q.Handler();
            return Ok(res);
        }
        [HttpGet("{id}")] //by route
        public IActionResult GetByID(int id)
        {
            var book = new BookDetailViewModel();
            try
            {
                GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_dbContext);
                getBookDetailQuery.BookId = id;
                book = getBookDetailQuery.Handler();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
            return Ok(book);
    
        }
        //[HttpGet] //from query
        //public Book Get([FromQuery] string id)
        //{
        //    /* There can be only one parameterless HttpGet in the code. Therefore, in order for this endpoint to function, the first endpoint should be disabled.s*/
        //    var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            try
            {
                CreateBookCommand createBookCommand = new CreateBookCommand(_dbContext);
                createBookCommand.Model = newBook;
                createBookCommand.Handler();

                
            }
            catch (Exception ex) 
            {
                
            }
            return Ok();


        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook) { 
            try
            {
                UpdateBookCommand updateBookCommand = new UpdateBookCommand(_dbContext);
                updateBookCommand.BookId = id;
                updateBookCommand.model = updatedBook;
                updateBookCommand.Handler();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_dbContext);
                deleteBookCommand.BookId = id;
                deleteBookCommand.Handler();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
