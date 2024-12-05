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
        internal readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
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
                var result = await _mediator.Send(new AddAuthorCommand(authorToAdd));

                if (result.Success)
                {
                    return CreatedAtAction(
                        nameof(GetAuthorById),
                        new { authorId = result.Data.Id },
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
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _mediator.Send(new GetAllAuthorsQuery());
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            try
            {
                var author = await _mediator.Send(new GetAuthorByIdQuery(authorId));
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
                var result = await _mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId));

                if (result.Success)
                {
                    return Ok(new { message = result.Message, data = result.Data });
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



        [Authorize]
        [HttpDelete]
        [Route("deleteAuthor/{authorToDeleteId}")]
        public async Task<IActionResult> DeleteAuthor(Guid authorToDeleteId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId));

                if (result.Success)
                {
                    return Ok(new { message = result.Message, data = result.Data });
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


        private IActionResult HandleError(Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }
}