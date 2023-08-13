using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

    }
}
