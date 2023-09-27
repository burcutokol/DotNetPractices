using AutoMapper;
using BookStoreWebApi.DbOperations;
using System;
using System.Linq;

namespace BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId;
        public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AuthorDetailModel Handler()
        {
            var Author = _dbContext.Authors.SingleOrDefault(x => x.Id  == AuthorId);
            if(Author == null) 
            {
                throw new InvalidOperationException("Girilen id'ye sahip yazar bulunamadı.");
            }
            AuthorDetailModel returnAuthor = _mapper.Map<AuthorDetailModel>(Author);
            return returnAuthor;
        }
    }
    public class AuthorDetailModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
