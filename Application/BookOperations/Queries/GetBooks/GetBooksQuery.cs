using AutoMapper;
using BookStoreWebApi.Common;
using BookStoreWebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
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
            var bookList = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList(); //Books içinde bulunan
                                                                                               //Genre entitysi include edildi.
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
 
            return vm;
        }
    }
    public class BookViewModel //viewmodel sadece uı' dönerken kullanılır.
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int PageCount { get; set; }

        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Genre { get; set; }
    }
}
