using AutoMapper;
using BookStoreWebApi.Application.GenreOperations;
using BookStoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using BookStoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using BookStoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using BookStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreWebApi.Application.GenreOperations.Queries.GetGenres;
using BookStoreWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext; //sadece contstructorda set edilebilir.
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery q = new GetGenresQuery(_dbContext, _mapper);
            var res = q.Handler();
            return Ok(res);
        }
        [HttpGet("id")] //by route
        public IActionResult GetGenreById( int id) 
        {
            GetGenreDetailsQuery q = new GetGenreDetailsQuery(_dbContext, _mapper);
            q.GenreId = id;
            GetGenreDetailsQueryValidator validationRules = new GetGenreDetailsQueryValidator();    
            validationRules.ValidateAndThrow(q);
            
            return Ok(q.Handler());

        }
        [HttpPost]
        public IActionResult AddGenre([FromBody]CreateGenreModel model)
        {
            CreateGenreCommand c = new CreateGenreCommand(_dbContext);
            c.Model = model;

            CreateGenreCommandValidator validationRules = new CreateGenreCommandValidator();    
            validationRules.ValidateAndThrow(c);

            c.Handler();

            return Ok();
        }
        [HttpPut("id")]
        public IActionResult UpdateGenre([FromBody] UpdateGenreModel model, int id) 
        { 
            UpdateGenreCommand c = new UpdateGenreCommand(_dbContext);
            c.GenreId = id;
            c.model = model;

            UpdateGenreCommandValidator validationRules = new UpdateGenreCommandValidator();    
            validationRules.ValidateAndThrow(c);

            c.Handler();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id) {
            DeleteGenreCommand c = new DeleteGenreCommand(_dbContext);
            c.GenreId = id;

            DeleteGenreCommandValidator validationRules= new DeleteGenreCommandValidator(); 
            validationRules.ValidateAndThrow(c);

            c.Handler();
            return Ok();
        }

    }
}
