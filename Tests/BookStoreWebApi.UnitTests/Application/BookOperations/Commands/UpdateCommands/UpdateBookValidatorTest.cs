using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreWebApi.UnitTests.Application.BookOperations.Commands.UpdateCommands
{
    public class UpdateBookValidatorTest : IClassFixture<CommonTestFixture>
    {
        [InlineData(-1, "Test", 1,1)]
        [InlineData(1, "Test", -1, 1)]
        [InlineData(1, "Test", 1, -1)]
        [InlineData(1, "", 1, 1)]
        [InlineData(1, "T", 1, 1)]

        [Theory]
        public void WhenInvalidInputsArGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int authorId, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.model = new UpdateBookViewModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId,
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var errors = validator.Validate(command);
            //assert
            errors.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.model = new UpdateBookViewModel()
            {
                Title = "test",
                GenreId = 1,
                AuthorId = 1,
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var errors = validator.Validate(command);
            //assert
            errors.Errors.Count.Should().Be(0);
        }
    }
}
