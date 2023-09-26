using AutoMapper;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model;
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handler()
        {
            var Author = _dbContext.Authors.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower());
            if (Author != null) 
            {
                throw new InvalidOperationException("Eklenmek istenen yazar zaten mevcut.");
            }
            Author = _mapper.Map<Author>(Model);

            _dbContext.Add(Author);
            _dbContext.SaveChanges();

        }

    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
