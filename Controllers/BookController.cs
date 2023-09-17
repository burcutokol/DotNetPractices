using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.DbOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext; //sadece contstructorda set edilebilir.
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

    
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery q = new GetBooksQuery(_dbContext, _mapper);
            var res = q.Handler();
            return Ok(res);
        }
        [HttpGet("{id}")] //by route
        public IActionResult GetByID(int id)
        {
            var book = new BookDetailViewModel();
            try
            {
                GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_dbContext, _mapper);
                getBookDetailQuery.BookId = id;
                GetBookDetailValidator validationRules = new GetBookDetailValidator();
                validationRules.ValidateAndThrow(getBookDetailQuery);
                book = getBookDetailQuery.Handler();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
            return Ok(book);
    
        }
     
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_dbContext, _mapper);
            CreateBookCommandValidator validationRules = new CreateBookCommandValidator();
            try
            {
                
                createBookCommand.Model = newBook;
                ValidationResult res =validationRules.Validate(createBookCommand);
                validationRules.ValidateAndThrow(createBookCommand);
                createBookCommand.Handler();

                
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok();


        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook) { 
            try
            {
                UpdateBookCommand updateBookCommand = new UpdateBookCommand(_dbContext);
                updateBookCommand.BookId = id;
                updateBookCommand.model = updatedBook;
                UpdateBookCommandValidator validationRules = new UpdateBookCommandValidator();
                validationRules.ValidateAndThrow(updateBookCommand);
                updateBookCommand.Handler();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_dbContext);
                deleteBookCommand.BookId = id;
                DeleteBookCommandValidator validationRules = new DeleteBookCommandValidator();  
                validationRules.ValidateAndThrow(deleteBookCommand);
                deleteBookCommand.Handler();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
