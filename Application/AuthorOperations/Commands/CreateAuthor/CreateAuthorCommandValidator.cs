using FluentValidation;
using System;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator() 
        {
            RuleFor(c => c.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(c => c.Model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(c => c.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date);

        }   

    }
}
