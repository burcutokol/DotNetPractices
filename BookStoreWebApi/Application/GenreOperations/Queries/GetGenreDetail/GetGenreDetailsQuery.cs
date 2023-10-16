using AutoMapper;
using BookStoreWebApi.Application.GenreOperations.Queries.GetGenres;
using BookStoreWebApi.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailsQuery
    {
        public int GenreId;
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenreDetailsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handler()
        {
            var Genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if(Genre == null) 
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            GenreDetailViewModel genre = _mapper.Map<GenreDetailViewModel>(Genre);
            return genre;
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }  
        public string Name { get; set; }

    }
}
