using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using System;
using System.Linq;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId;
        public UpdateAuthorModel model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateAuthorCommand(BookStoreDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }    
        public void Handler()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author == null) 
            {
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı.");
            }
            if(_dbContext.Authors.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.Surname.ToLower() == model.Surname.ToLower() && x.Id != AuthorId) )
            {
                throw new InvalidOperationException("Bu yazar zaten mevcut.");
            }
            author.Name = model.Name.Trim() == "string" || string.IsNullOrEmpty(model.Name.Trim()) ? author.Name : model.Name;
            author.Surname = model.Surname.Trim() == "string" || string.IsNullOrEmpty(model.Surname.Trim()) ? author.Surname : model.Surname;
            author.BirthDate = model.BirthDate;

            _dbContext.SaveChanges();



        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
