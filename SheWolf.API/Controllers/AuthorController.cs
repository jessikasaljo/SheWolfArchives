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

        [Authorize]
        [HttpPost]
        [Route("addNewAuthor")]
        public async Task<IActionResult> AddNewAuthor([FromBody] Author authorToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await mediator.Send(new AddAuthorCommand(authorToAdd));
                return CreatedAtAction(nameof(GetAuthorsById), new { authorId = result.Id }, result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await mediator.Send(new GetAllAuthorsQuery());
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<IActionResult> GetAuthorsById(Guid authorId)
        {
            try
            {
                var author = await mediator.Send(new GetAuthorByIdQuery(authorId));
                if (author == null)
                {
                    return NotFound(new { Message = "Author not found." });
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateAuthor/{updatedAuthorId}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author updatedAuthor, Guid updatedAuthorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteAuthor/{authorToDeleteId}")]
        public async Task<IActionResult> DeleteAuthor([FromBody] Guid authorToDeleteId)
        {
            try
            {
                var result = await mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId));
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