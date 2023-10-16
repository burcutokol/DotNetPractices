
using BookStoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using BookStoreWebApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreWebApi.UnitTests.Application.GenreOperations.Commands.CreateGenreCommands
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("ABC")]
        public void WhenInvalidInputsArGiven_Validator_ShouldBeReturnErrors(string genreName)
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
            
            command.Model = new CreateGenreModel { Name = genreName };
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var error = validator.Validate(command);
            error.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}
