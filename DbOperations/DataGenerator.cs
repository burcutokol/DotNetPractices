using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using BookStoreWebApi.Entities;


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
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Roman"
                    },
                    new Genre
                    {
                        Name = "Bilim Kurgu"
                    },
                    new Genre
                    {
                        Name = "Kişisel Gelişim"
                    }
                    );
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

                context.AddRange(
                    new Author
                    {
                        Name = "Sabahattin",
                        Surname = "Ali",
                        BirthDate = new DateTime(1907, 02, 25),

                    },
                    new Author
                    {
                        Name = "Isaac",
                        Surname = "Asimov",
                        BirthDate = new DateTime(1920, 01, 02)
                    },
                    new Author
                    {
                        Name = "George",
                        Surname  = "Orwell",
                        BirthDate = new DateTime(1903, 06 , 25),
                    }
                    );

                context.SaveChanges();
            }


        }
    }
}
