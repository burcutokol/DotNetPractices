using FluentValidation;
using System.Globalization;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator() 
        {
            RuleFor(c => c.model.Name).MinimumLength(3).When(x => x.model.Name.Trim() != string.Empty);
            RuleFor(c => c.model.Surname).MinimumLength(3).When(x => x.model.Surname.Trim() != string.Empty);
            RuleFor(c => c.AuthorId).NotEmpty().GreaterThan(0);
        }   
    }
}
