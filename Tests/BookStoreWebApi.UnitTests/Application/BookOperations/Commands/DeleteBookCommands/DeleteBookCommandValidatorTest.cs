
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreWebApi.UnitTests.Application.BookOperations.Commands.DeleteBookCommands
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors()
        {
          
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = -9;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();    
            var errors = validator.Validate(command);
            errors.Errors.Count.Should().BeGreaterThan(0);


        }
        [Fact]
        public void WhenvalidIdIsGiven_Validator_ShouldNotReturnError()
        {

            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var errors = validator.Validate(command);
            errors.Errors.Count.Should().Be(0);


        }
    }
}
