using BookStoreWebApi.DbOperations;
using System;
using System.Linq;

namespace BookStoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookViewModel model { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı.");       
            



            book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
            book.Title = model.Title != "string" ? model.Title : book.Title;
          
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set;}
    }

}
