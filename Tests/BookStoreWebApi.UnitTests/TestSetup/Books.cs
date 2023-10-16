using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Kürk Mantolu Madonna",
                        GenreId = 1, //roman
                        PageCount = 177,
                        PublishDate = new DateTime(1943, 01, 01),
                        AuthorId = 1,
                    },
                   new Book
                   {
                       // Id = 2,
                       Title = "Ben, Robot",
                       GenreId = 2, //bilim kurgu
                       PageCount = 240,
                       PublishDate = new DateTime(1950, 02, 1),
                       AuthorId = 2,
                   },
                   new Book
                   {
                       //Id = 3,
                       Title = "1984",
                       GenreId = 2, //bilim kurgu
                       PageCount = 352,
                       PublishDate = new DateTime(1949, 08, 06),
                       AuthorId = 3,
                   });
        }
    }
}
