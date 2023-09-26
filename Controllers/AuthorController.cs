using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStoreWebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext; //sadece contstructorda set edilebilir.
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_dbContext, _mapper);
            List<AuthorViewModel> list = query.Handler();
            return Ok(list);
        }
        [HttpGet("id")]
        public IActionResult GetAuthorById(int id) 
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbContext, _mapper);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validationRules = new GetAuthorDetailQueryValidator();
            validationRules.ValidateAndThrow(query);

            return Ok(query.Handler()); 
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel model)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper); 
            command.Model = model;

            CreateAuthorCommandValidator validationRules = new CreateAuthorCommandValidator();  
            validationRules.ValidateAndThrow(command);

            command.Handler();
            return Ok();   

        }
        [HttpPut("id")]
        public IActionResult UpdateAuthor([FromBody] UpdateAuthorModel model, int id)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
            command.AuthorId = id;
            command.model = model;

            UpdateAuthorCommandValidator validationRules = new UpdateAuthorCommandValidator();
            validationRules.ValidateAndThrow(command);

            command.Handler();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAuthor(int id) 
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validationRules = new DeleteAuthorCommandValidator();  
            validationRules.ValidateAndThrow(command);

            command.Handler();
            return Ok();
        }
    }
}
