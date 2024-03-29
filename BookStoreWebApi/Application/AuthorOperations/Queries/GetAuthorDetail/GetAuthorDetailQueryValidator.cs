﻿using FluentValidation;

namespace BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator() 
        {
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
}
