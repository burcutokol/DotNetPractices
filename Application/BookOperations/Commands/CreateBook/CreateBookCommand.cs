using AutoMapper;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;

namespace BookStoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }  
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book != null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            if(!(_dbContext.Genres.Any(x => x.Id == Model.GenreId)))
            {
                throw new InvalidOperationException("Bu id'ye ait kitap türü yok. Önce kitap türünü oluşturmalısınız.");
            }
            if (!(_dbContext.Authors.Any(x => x.Id == Model.AuthorId)))
            {
                throw new InvalidOperationException("Bu id'ye ait yazar yok. Önce yazarı oluşturmalısınız.");
            }
            book = _mapper.Map<Book>(Model); //model convert to book


            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
        
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }    
        public DateTime PublishDate { get; set; }
    }

}
