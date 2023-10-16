using AutoMapper;
using BookStoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreWebApi.UnitTests.Application.GenreOperations.Commands.CreateGenreCommands
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                IsActive = true,
                Name = "WhenAlreadyExistsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel() { Name = genre.Name};
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handler()) //testte çalışması gereken metot
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap türü zaten mecvut");

        }
        [Fact]
        public void WhenValidGenreNameIsGiven_Ok_ShouldBeReturn()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel() { Name = "WhenAlreadyExistsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn" };
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handler()) //testte çalışması gereken metot
                .Should().BeOfType<System.Action>();

        }
    }
}
