using BookStoreWebApi.Common;
using BookStoreWebApi.DbOperations;
using System;
using System.Linq;

namespace BookStoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        } 
        public BookDetailViewModel Handler()
        {
            var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException("Bu id'ye sahip kitap yok.");
            BookDetailViewModel viewModel = new BookDetailViewModel();
            viewModel.PublishDate = DateTime.Parse(book.PublishDate.Date.ToString("dd/MM/yyy"));
            viewModel.Title = book.Title;
            viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
            return viewModel;

        }
    }
    public class BookDetailViewModel //viewmodel sadece uı' dönerken kullanılır.
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int PageCount { get; set; }

        public string Genre { get; set; }
    }
}
