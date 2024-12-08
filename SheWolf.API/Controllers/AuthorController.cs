using SheWolf.Application.Commands.Authors.AddAuthor;
using SheWolf.Application.Commands.Authors.DeleteAuthor;
using SheWolf.Application.Commands.Authors.UpdateAuthor;
using SheWolf.Application.Queries.Authors.GetAll;
using SheWolf.Application.Queries.Authors.GetById;
using SheWolf.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SheWolf.Application.DTOs;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        internal readonly IMediator _mediator;
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IMediator mediator, IAuthorRepository authorRepository)
        {
            _mediator = mediator;
            _authorRepository = authorRepository;
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
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _mediator.Send(new GetAllAuthorsQuery());
                var authorDtos = authors.Data.Select(author => new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name,
                    BookTitles = author.BookTitles
                }).ToList();

                return Ok(authorDtos);
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
                var operationResult = await _mediator.Send(new GetAuthorByIdQuery(authorId));
                var author = operationResult.Data;

                if (author == null)
                {
                    return NotFound(new { Message = "Author not found." });
                }

                var authorDto = new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name,
                    BookTitles = author.BookTitles
                };

                return Ok(new { message = "Author retrieved successfully", data = authorDto });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("updateAuthor/{updatedAuthorId}")]
        public async Task<IActionResult> UpdateAuthor(Guid updatedAuthorId, [FromBody] AuthorDto updatedAuthorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedAuthor = await _authorRepository.UpdateAuthor(updatedAuthorId, updatedAuthorDto);

                if (updatedAuthor == null)
                {
                    return NotFound(new { Message = "Author not found." });
                }

                return Ok(new { message = "Author updated successfully", data = updatedAuthor });
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
            return StatusCode(500, new
            {
                message = "An error occurred while processing your request.",
                details = ex.Message,
                innerException = ex.InnerException?.Message
            });
        }
    }
}
