using FluentValidation;

namespace BookStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailsQueryValidator : AbstractValidator<GetGenreDetailsQuery>
    {
        public GetGenreDetailsQueryValidator() 
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}
