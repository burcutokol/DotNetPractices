using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebApi.UnitTests.TestSetup
{
    public static  class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
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
                       Surname = "Orwell",
                       BirthDate = new DateTime(1903, 06, 25),
                   }
                   );
        }
    }
}
