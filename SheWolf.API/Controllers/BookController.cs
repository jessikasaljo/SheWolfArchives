using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SheWolf.Application.Commands.Books.AddBook;
using SheWolf.Application.Commands.Books.DeleteBook;
using SheWolf.Application.Commands.Books.UpdateBook;
using SheWolf.Application.Queries.Books.GetAll;
using SheWolf.Application.Queries.Books.GetById;
using SheWolf.Domain.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        internal readonly IMediator mediator;

        public BookController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("addNewBook")]
        public async Task<IActionResult> AddNewBook([FromBody] Book bookToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await mediator.Send(new AddBookCommand(bookToAdd));
                return CreatedAtAction(nameof(GetBookById), new { bookId = result.Id }, result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await mediator.Send(new GetAllBooksQuery());
                return Ok(books);
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
                var book = await mediator.Send(new GetBookByIdQuery(bookId));
                if (book == null)
                {
                    return NotFound(new { Message = "Book not found." });
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateBook/{updatedBookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] Book updatedBook, Guid updatedBookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await mediator.Send(new UpdateBookByIdCommand(updatedBook, updatedBookId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteBook/{bookToDeleteId}")]
        public async Task<IActionResult> DeleteBook([FromBody] Guid bookToDeleteId)
        {
            try
            {
                var result = await mediator.Send(new DeleteBookByIdCommand(bookToDeleteId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        private IActionResult HandleError(Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }
}