using FluentValidation;

namespace BookStoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailValidator() 
        {
            RuleFor(q => q.BookId).GreaterThan(0);
        }     
    }
}
