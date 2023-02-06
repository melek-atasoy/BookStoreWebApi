using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.Controllers 
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /* private static List<Book> BookList = new List<Book>() {
            new Book {
                Id = 1,
                Title = "Budala",
                GenreId = 1,
                PageCount = 1050,
                PublishDate = new DateTime(1997, 06, 05)
            },
            new Book {
                Id = 2,
                Title = "Fareler ve Insanlar",
                GenreId = 2,
                PageCount = 350,
                PublishDate = new DateTime(2002, 05, 14)
            },
            new Book {
                Id = 3,
                Title = "1984",
                GenreId = 3,
                PageCount = 300,
                PublishDate = new DateTime(1991, 04, 23)
            }
        }; */

        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id){
            
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                query.BookId = id;
                validator.ValidateAndThrow(query);

                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // [HttpGet]
        // public Book GetBookById([FromQuery] string id){
        //     var book = BookList.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
        //     return book;
        // }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                command.Model = newBook;
                validator.ValidateAndThrow(command);
                //ValidationResult result = validator.Validate(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook){
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.UpdatedBook = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
        
    }
}