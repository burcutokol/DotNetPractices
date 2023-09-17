using FluentValidation;

namespace BookStoreWebApi.BookOperations.DeleteBook
{
    
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator() 
        {
            RuleFor(c => c.BookId).GreaterThan(0);
        } 
    }
}
