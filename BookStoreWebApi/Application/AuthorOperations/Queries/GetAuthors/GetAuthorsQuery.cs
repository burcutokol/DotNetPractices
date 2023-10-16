using AutoMapper;
using BookStoreWebApi.DbOperations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<AuthorViewModel> Handler() 
        {
            var AuthorList = _dbContext.Authors.OrderBy(x => x.Id).ToList();
            List<AuthorViewModel> list = _mapper.Map<List<AuthorViewModel>>(AuthorList);
            return list;
        }
        
    }
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; } 
    }
}
