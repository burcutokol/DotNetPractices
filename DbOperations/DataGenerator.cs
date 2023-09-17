using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStoreWebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                new Book
                {
                    //Id = 1,
                    Title = "Kürk Mantolu Madonna",
                    GenreId = 1, //roman
                    PageCount = 177,
                    PublishDate = new System.DateTime(1943, 01, 01),
                },
               new Book
               {
                  // Id = 2,
                   Title = "Ben, Robot",
                   GenreId = 2, //bilim kurgu
                   PageCount = 240,
                   PublishDate = new System.DateTime(1950, 02, 1),
               },
               new Book
               {
                   //Id = 3,
                   Title = "1984",
                   GenreId = 2, //bilim kurgu
                   PageCount = 352,
                   PublishDate = new System.DateTime(1949, 08, 06),
               });

                context.SaveChanges();
            }


        }
    }
}
