using BookStoreWebApi.DbOperations;
using System;
using System.Linq;

namespace BookStoreWebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId;
        public UpdateGenreModel model;
        private readonly IBookStoreDbContext _dbContext;
        public UpdateGenreCommand(IBookStoreDbContext dbContext)
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
            if(_dbContext.Genres.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != GenreId) ) 
            {
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mecvut.");  
            }
            genre.IsActive = model.IsActive;
            genre.Name =  model.Name.Trim() == "string" || string.IsNullOrEmpty(model.Name.Trim()) ?  genre.Name : model.Name ;
            _dbContext.SaveChanges();

        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
