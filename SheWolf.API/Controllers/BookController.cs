using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Commands.Books.AddBook;
using SheWolf.Application.Commands.Books.DeleteBook;
using SheWolf.Application.Commands.Books.UpdateBook;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;
using SheWolf.Application.Queries.Authors.GetById;
using SheWolf.Application.Queries.Books.GetAll;
using SheWolf.Application.Queries.Books.GetById;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        internal readonly IMediator _mediator;
        private readonly SheWolf_Database _database;

        public BookController(IMediator mediator, SheWolf_Database database)
        {
            _mediator = mediator;
            _database = database;
        }

        [Authorize]
        [HttpPost]
        [Route("addNewBook")]
        public async Task<IActionResult> AddNewBook([FromBody] BookDto bookToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (bookToAdd == null)
                {
                    return BadRequest(new { message = "Book data is required." });
                }

                var operationResult = await _mediator.Send(new GetAuthorByIdQuery(bookToAdd.AuthorId));
                var authorDto = operationResult.Data;

                if (authorDto == null)
                {
                    return BadRequest(new { message = "Author not found." });
                }

                var author = EntityMapper.MapToEntity(authorDto);

                if (_database.ChangeTracker.Entries<Author>().Any(e => e.Entity.Id == author.Id))
                {
                    author = _database.Authors.Local.FirstOrDefault(a => a.Id == author.Id);
                }

                var book = EntityMapper.MapToEntity(bookToAdd, author);

                var result = await _mediator.Send(new AddBookCommand(book));

                if (!result.Success)
                {
                    return BadRequest(new { message = result.Message, errorMessage = result.ErrorMessage });
                }

                return CreatedAtAction(
                    nameof(GetBookById),
                    new { bookId = result.Data.Id },
                    new { message = result.Message, data = result.Data });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }



        [HttpGet]
        [Route("getAllBooks")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _mediator.Send(new GetAllBooksQuery());

                if (books.Data == null || !books.Data.Any())
                {
                    return Ok(new { message = "No books found.", data = new List<BookDto>() });
                }

                var bookDtos = books.Data.Select(b => EntityMapper.MapToDto(b)).ToList();
                return Ok(new { message = "Books retrieved successfully", data = bookDtos });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetBookByIdQuery(bookId));

                if (operationResult.Data == null)
                {
                    return NotFound(new { Message = "Book not found." });
                }

                var bookDto = EntityMapper.MapToDto(operationResult.Data);
                return Ok(new { message = operationResult.Message, data = bookDto });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateBook/{updatedBookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto updatedBook, Guid updatedBookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var operationResult = await _mediator.Send(new GetAuthorByIdQuery(updatedBook.AuthorId));

                if (!operationResult.Success || operationResult.Data == null)
                {
                    return BadRequest(new { message = "Author not found." });
                }

                var authorDto = EntityMapper.MapToEntity(operationResult.Data);
                var book = EntityMapper.MapToEntity(updatedBook, authorDto);
                var updateResult = await _mediator.Send(new UpdateBookByIdCommand(book, updatedBookId));

                if (updateResult.Success)
                {
                    return Ok(new { message = "Book updated successfully", data = updateResult.Data });
                }
                else
                {
                    return BadRequest(new { message = updateResult.Message, errorMessage = updateResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("deleteBook/{bookToDeleteId}")]
        public async Task<IActionResult> DeleteBook(Guid bookToDeleteId)
        {
            try
            {
                var operationResult = await _mediator.Send(new DeleteBookByIdCommand(bookToDeleteId));

                if (operationResult.Success)
                {
                    return Ok(new { message = "Successfully deleted book", data = operationResult.Data });
                }
                else
                {
                    return BadRequest(new { message = operationResult.Message, errorMessage = operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        private IActionResult HandleError(Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while processing your request.",
                details = ex.Message,
                innerException = ex.InnerException?.Message
            });
        }
    }
}
