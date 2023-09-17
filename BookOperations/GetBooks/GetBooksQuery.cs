using AutoMapper;
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
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BookViewModel> Handler()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
 
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
