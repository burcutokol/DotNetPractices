using FluentValidation;

namespace BookStoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator() 
        {
            RuleFor(c => c.BookId).GreaterThan(0);
            RuleFor(c => c.model.AuthorId).GreaterThan(0);
            RuleFor(c=> c.model.GenreId).GreaterThan(0);
            RuleFor(c => c.model.Title).NotEmpty().MinimumLength(2);

        } 
    }
}
