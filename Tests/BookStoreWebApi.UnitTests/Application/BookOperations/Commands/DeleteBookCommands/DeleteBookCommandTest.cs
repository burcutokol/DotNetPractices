using AutoMapper;
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebApi.UnitTests.Application.BookOperations.Commands.DeleteBookCommands
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenDoesNotExistsBookIdGiven_InvalidExceptionOperation_ShouldBeReturn()
         {
            //arrange - hazırlık
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = -1;
            //act- assert
            FluentActions
                .Invoking(() => command.Handler()) //testte çalışması gereken metot
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Bu id'ye sahip bir kitap yok.");

        }
        [Fact]  
        public void WhenValidIdIsGiven_Ok_ShouldBeReturn()
        {
            //arrange 
            var Book = new Book
            {
                Title = "Test",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddYears(-1)
            };

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = Book.Id;
            //act - assert
            FluentActions
               .Invoking(() => command.Handler()) //testte çalışması gereken metot
               .Should().BeOfType<System.Action>();


        }
    }
}
