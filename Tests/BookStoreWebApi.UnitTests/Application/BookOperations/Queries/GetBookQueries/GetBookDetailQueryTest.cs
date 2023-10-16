using AutoMapper;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStoreWebApi.UnitTests.Application.BookOperations.Queries.GetBookQueries
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void When_InvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var BookId = -1;
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = BookId;

            FluentActions.Invoking(() => query.Handler())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Bu id'ye sahip kitap yok.");
        }
        [Fact]  
        public void When_ValidBookIdIsGiven_Book_ShouldBeReturn()
        {
            var Book = new Book
            {
                Title = "Hobbit",
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 1,
            };
            _context.Books.Add(Book);
            _context.SaveChanges();
            GetBookDetailQuery query = new GetBookDetailQuery( _context,_mapper);
            query.BookId = Book.Id;

            var model = _context.Set<Book>().SingleOrDefault(x => x.Id == query.BookId);
            FluentActions.Invoking(() => query.Handler()).Invoke();
            model.Should().NotBeNull();



        }
    }
}
