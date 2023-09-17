using BookStoreWebApi.Common;
using BookStoreWebApi.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreWebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public List<BookViewModel> Handler()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BookViewModel()
                {
                    Title = book.Title,
                    PageCount = book.PageCount,
                    PublishDate = DateTime.Parse(book.PublishDate.Date.ToString("dd/MM/yyy")),
                    Genre = ((GenreEnum)book.GenreId).ToString(),


                }); ;
            }
            return vm;
        }
    }
    public class BookViewModel //viewmodel sadece uı' dönerken kullanılır.
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int PageCount { get; set; }

        public string Genre { get; set; }
    }
}
