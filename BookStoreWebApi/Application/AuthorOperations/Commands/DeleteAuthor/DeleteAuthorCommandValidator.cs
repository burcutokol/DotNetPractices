using FluentValidation;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator() 
        {
            RuleFor(c => c.AuthorId).NotEmpty().GreaterThan(0);
        }   
    }
}
