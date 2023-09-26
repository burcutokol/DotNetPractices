using FluentValidation;

namespace BookStoreWebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator() 
        {
            RuleFor(command => command.model.Name).MinimumLength(4).When(x => x.model.Name.Trim() != string.Empty); //when model name is not null,
                                                                                                                    //model name contains at least 4 chars.
                                                                                                                    //if model name is null, it controls on handler.
        }
    }
}
