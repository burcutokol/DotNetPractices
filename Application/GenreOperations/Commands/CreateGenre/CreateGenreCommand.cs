using AutoMapper;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStoreWebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; } 
        private readonly BookStoreDbContext _dbContext;
        public CreateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handler()
        {
            var Genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if(Genre != null) 
            {
                throw new InvalidOperationException("Kitap türü zaten mecvut");
            }
            Genre = new Genre();
            Genre.Name = Model.Name;    
            _dbContext.Genres.Add(Genre);
            _dbContext.SaveChanges();
        }
    }
    public class CreateGenreModel
    {
        public string Name { get; set; }    
    }
}
