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
            if (!(_dbContext.Genres.Any(x => x.Id == model.GenreId)))
            {
                throw new InvalidOperationException("Bu id'ye ait kitap türü yok. Önce kitap türünü oluşturmalısınız.");
            }
            if (!(_dbContext.Authors.Any(x => x.Id == model.AuthorId)))
            {
                throw new InvalidOperationException("Bu id'ye ait yazar yok. Önce yazarı oluşturmalısınız.");
            }



            book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
            book.AuthorId = model.AuthorId != default ? model.AuthorId : book.AuthorId;
            book.Title = model.Title != "string" ? model.Title : book.Title;
          
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set;}
        public int AuthorId { get; set; }
    }

}
