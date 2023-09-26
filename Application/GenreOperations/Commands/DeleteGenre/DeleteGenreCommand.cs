using BookStoreWebApi.DbOperations;
using System;
using System.Linq;

namespace BookStoreWebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId;
        private readonly BookStoreDbContext _dbContext;
        public DeleteGenreCommand(BookStoreDbContext dbContext) 
        { 
            _dbContext = dbContext;
        } 
        public void Handler()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);        
            if (genre == null) 
            {
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            }
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}
