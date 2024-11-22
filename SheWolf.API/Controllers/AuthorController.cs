using SheWolf.Application.Commands.Authors.AddAuthor;
using SheWolf.Application.Commands.Authors.DeleteAuthor;
using SheWolf.Application.Commands.Authors.UpdateAuthor;
using SheWolf.Application.Queries.Authors.GetAll;
using SheWolf.Application.Queries.Authors.GetById;
using SheWolf.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SheWolf.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        internal readonly IMediator mediator;

        public AuthorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //[Authorize]
        [HttpPost]
        [Route("addNewAuthor")]
        public async void AddNewAuthor([FromBody] Author authorToAdd)
        {
            await mediator.Send(new AddAuthorCommand(authorToAdd));
        }

        [HttpGet]
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await mediator.Send(new GetAllAuthorsQuery()));
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<IActionResult> GetAuthorsById(Guid authorId)
        {
            return Ok(await mediator.Send(new GetAuthorByIdQuery(authorId)));
        }

        //[Authorize]
        [HttpPut]
        [Route("updateAuthor/{updatedAuthorId}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author updatedAuthor, Guid updatedAuthorId)
        {
            return Ok(await mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId)));
        }

        //[Authorize]
        [HttpDelete]
        [Route("deleteAuthor/{authorToDeleteId}")]
        public async Task<IActionResult> DeleteAuthor([FromBody] Guid authorToDeleteId)
        {
            return Ok(await mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId)));
        }
    }
}