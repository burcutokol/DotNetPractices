using BookStoreWebApi.DbOperations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId;
        private readonly IBookStoreDbContext _dbContext;
        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handler()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author == null)
            {
                throw new InvalidOperationException("Silinmek istenen yazar bulunamadı.");
            }
            if((_dbContext.Books.Any(x => x.AuthorId == AuthorId)))
            {
                throw new InvalidOperationException("Bu yazarın kitabı var. Önce kitaplarını silmelisiniz.");
            }
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
