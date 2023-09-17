﻿using BookStoreWebApi.DbOperations;
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
        public CreateBookCommand(BookStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book != null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            book = new Book();
            book.Title = Model.Title;   
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
        
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }    
        public DateTime PublishDate { get; set; }
    }

}
