using AutoMapper;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreWebApi.UnitTests.Application.BookOperations.Commands.UpdateCommands
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;    
        }
        [Fact]
        public void WhenAInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange - hazırlık
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = -9;

            //act - çalıştırma  && assert - doğrulama
            FluentActions
                .Invoking(() => command.Handler()) //testte çalışması gereken metot
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellenecek kitap bulunamadı.");
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn() 
        {
            //arrange - hazırlık
            UpdateBookCommand command = new UpdateBookCommand(_context);
            var Book = new Book
            {
                Title = "Test",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddMonths(-1)
            };
            _context.Books.Add(Book);
            _context.SaveChanges(); 
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "Hobbit",
                GenreId = -1,
                AuthorId = 1,
            };
            command.BookId = Book.Id;
            command.model = model;
            // act --assert
            FluentActions.Invoking(() => command.Handler()).
                Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Bu id'ye ait kitap türü yok. Önce kitap türünü oluşturmalısınız."); ;
           
            
        }
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange - hazırlık
            UpdateBookCommand command = new UpdateBookCommand(_context);
            var Book = new Book
            {
                Title = "Test",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddMonths(-1)
            };
            _context.Books.Add(Book);
            _context.SaveChanges();
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "Hobbit",
                GenreId = 1,
                AuthorId = -1,
            };
            command.BookId = Book.Id;
            command.model = model;
            // act --assert
            FluentActions.Invoking(() => command.Handler()).
                Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Bu id'ye ait yazar yok. Önce yazarı oluşturmalısınız."); ;


        }
        [Fact]
        public void WhenValidInputIsGiven_Ok_ShouldBeUpdated() //happy path
        {
            //arrange - hazırlık
            UpdateBookCommand command = new UpdateBookCommand(_context);
            var Book = new Book
            {
                Title = "Test",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddMonths(-1)
            };
            _context.Books.Add(Book);
            _context.SaveChanges();
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "Hobbit",
                GenreId = 1,
                AuthorId = 1,
            };
            command.BookId = Book.Id;
            command.model = model;
            // act
            FluentActions.Invoking(() => command.Handler()).Invoke();
            //assert
            var book = _context.Set<Book>().SingleOrDefault(x => x.Id == Book.Id);
            book.Should().NotBeNull(); //böyle bir kitap gelmeli, çünkü ekledik
            book.Title.Should().Be(model.Title);
            book.Author.Id.Should().Be(model.AuthorId);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}
