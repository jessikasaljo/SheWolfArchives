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

        //[Authorize]
        [HttpPost]
        [Route("addNewBook")]
        public async void AddNewBook([FromBody] Book bookToAdd)
        {
            await mediator.Send(new AddBookCommand(bookToAdd));
        }

        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await mediator.Send(new GetAllBooksQuery()));
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            return Ok(await mediator.Send(new GetBookByIdQuery(bookId)));
        }

        //[Authorize]
        [HttpPut]
        [Route("updateBook/{updatedBookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] Book updatedBook, Guid updatedBookId)
        {
            return Ok(await mediator.Send(new UpdateBookByIdCommand(updatedBook, updatedBookId)));
        }

        //[Authorize]
        [HttpDelete]
        [Route("deleteBook/{bookToDeleteId}")]
        public async Task<IActionResult> DeleteBook([FromBody] Guid bookToDeleteId)
        {
            return Ok(await mediator.Send(new DeleteBookByIdCommand(bookToDeleteId)));
        }

    }
}