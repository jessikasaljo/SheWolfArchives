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
        internal readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
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
                var result = await _mediator.Send(new AddBookCommand(bookToAdd));

                if (result.Success)
                {
                    return CreatedAtAction(
                        nameof(GetBookById),
                        new { bookId = result.Data.Id },
                        new { message = result.Message, data = result.Data });
                }
                else
                {
                    return BadRequest(new { message = result.Message, errorMessage = result.ErrorMessage });
                }
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
                var books = await _mediator.Send(new GetAllBooksQuery());
                return Ok(new { message = "Books retrieved successfully", data = books });
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

                if (operationResult.Success)
                {
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
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
                var operationResult = await _mediator.Send(new UpdateBookByIdCommand(updatedBook, updatedBookId));

                if (operationResult.Success)
                {
                    return Ok(new { message = "Book updated successfully", data = operationResult.Data });
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
            return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
        }
    }
}
