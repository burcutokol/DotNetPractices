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
        private static List<Book> BookList = new List<Book>()
       {
           new Book
           {
               Id= 1,
               Title = "Kürk Mantolu Madonna",
               GenreId = 1, //roman
               PageCount = 177,
               PublishDate = new System.DateTime(1943,01,01),
           },
           new Book
           {
               Id= 2,
               Title = "Ben, Robot",
               GenreId = 2, //bilim kurgu
               PageCount = 240,
               PublishDate = new System.DateTime(1950,02,1),
           },
           new Book
           {
               Id= 3,
               Title = "1984",
               GenreId = 2, //roman
               PageCount = 352,
               PublishDate = new System.DateTime(1949,08,06),
           }};
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList();
            return bookList;
        }
        [HttpGet("{id}")] //by route
        public Book GetByID(int id)
        {
            var book = BookList.Where(x => x.Id == id).SingleOrDefault();
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
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if (book != null)
                return BadRequest();
            BookList.Add(newBook);
            return Ok();

        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook) { 
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book == null)
                return BadRequest();
            /* The unchanged properties of the updated book that are received as parameters remain as their default values. 
             * If a property came as default, it means it should remain unchanged with its old value. 
             * If a property didn't come as default, meaning a value was provided, it should be updated with that value.*/
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;  
            book.Title = updatedBook.Title != "string" ? updatedBook.Title : book.Title;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount; 

            return Ok();
        }

    }
}
