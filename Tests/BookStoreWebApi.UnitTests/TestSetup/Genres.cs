using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
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
        }

    }
}
