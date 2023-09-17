using BookStoreWebApi.DbOperations;
using System;
using System.Linq;

namespace BookStoreWebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId;
        public DeleteBookCommand(BookStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }  
        public void Handler ()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new InvalidOperationException("Bu id'ye sahip bir kitap yok.");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
