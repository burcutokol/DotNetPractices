using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStoreWebApi.UnitTests.Application.BookOperations.Commands.CreateBookCommands
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("lord of the rings", 0, 0)]
        [InlineData("lord of the rings", 0, 1)]
        [InlineData("lord of the rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("lor", 100, 1)]
        [InlineData("lord", 100, 0)]
        [InlineData("lord", 0, 1)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsArGiven_Validator_ShouldBeReturnErrors(string title , int pageCount, int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId,
                AuthorId = 1,
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var errors = validator.Validate(command);
            //assert
            errors.Errors.Count.Should().BeGreaterThan(0);
            
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "lord of the ring" ,//sadece datimeda kırılmalı. diğer caseler diğer testte 
                PageCount = 100,
                PublishDate = DateTime.Now.Date, //hata versin diye
                GenreId = 1,
                AuthorId= 1,
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var errors = validator.Validate(command);
            errors.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "lord of the ring",//sadece datimeda kırılmalı. diğer caseler diğer testte 
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2), //hata versin diye
                GenreId = 1,
                AuthorId= 1,
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var errors = validator.Validate(command);
            errors.Errors.Count.Should().Be(0);
        }
    }
}
