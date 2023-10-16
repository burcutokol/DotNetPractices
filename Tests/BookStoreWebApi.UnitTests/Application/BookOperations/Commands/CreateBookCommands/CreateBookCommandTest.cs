using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreWebApi.UnitTests.Application.BookOperations.Commands.CreateCommands
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistsBsookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange - hazırlık
            var book = new Book()
            {
                Title = "WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            //act - çalıştırma  && assert - doğrulama
            FluentActions
                .Invoking(() => command.Handler()) //testte çalışması gereken metot
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap zaten mevcut");
        }
        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldNotBeCreated() //happy path
        {
            //arrange - hazırlık
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 1,
            };
            command.Model = model;
            // act
            FluentActions.Invoking(() => command.Handler()).Invoke();
            //assert
            var book = _context.Set<Book>().SingleOrDefault(x => x.Title == model.Title );
            book.Should().NotBeNull(); //böyle bir kitap gelmeli, çünkü ekledik
            book.PageCount.Should().Be(model.PageCount);
            book.Author.Id.Should().Be(model.AuthorId);    
            book.PublishDate.Should().Be(model.PublishDate);    
            book.GenreId.Should().Be(model.GenreId);    
        }
    }
}
