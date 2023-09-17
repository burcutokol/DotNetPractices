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
        public List<Book> GetBooks()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();
            return bookList;
        }
        [HttpGet("{id}")] //by route
        public Book GetByID(int id)
        {
            var book = _dbContext.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }
        //[HttpGet] //from query
        //public Book Get([FromQuery] string id)
        //{
        //    /* There can be only one parameterless HttpGet in the code. Therefore, in order for this endpoint to function, the first endpoint should be disabled.s*/
        //    var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == newBook.Title);
            if (book != null)
                return BadRequest();
            _dbContext.Books.Add(newBook);
            _dbContext.SaveChanges();
            return Ok();

        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook) { 
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if(book == null)
                return BadRequest();
            /* The unchanged properties of the updated book that are received as parameters remain as their default values. 
             * If a property came as default, it means it should remain unchanged with its old value. 
             * If a property didn't come as default, meaning a value was provided, it should be updated with that value.*/
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;  
            book.Title = updatedBook.Title != "string" ? updatedBook.Title : book.Title;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if(book == null)
                return BadRequest();
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
