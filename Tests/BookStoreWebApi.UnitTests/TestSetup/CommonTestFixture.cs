using AutoMapper;
using BookStoreWebApi.Common;
using BookStoreWebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; } 
        public CommonTestFixture() 
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName : "BookStoreTestDb").Options;
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();

            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();

        }  
    }
}
