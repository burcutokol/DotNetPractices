using AutoMapper;
using BookStoreWebApi.Common;
using BookStoreWebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        } 
        public BookDetailViewModel Handler()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(x => x.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException("Bu id'ye sahip kitap yok.");
            BookDetailViewModel viewModel = _mapper.Map<BookDetailViewModel>(book);
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
