using MediatR;
using Microsoft.AspNetCore.Mvc;
using SheWolf.Domain.Entities;
using SheWolf.Application.Queries.Users.GetAll;
using SheWolf.Application.Commands.Users.AddUser;
using SheWolf.Application.Queries.Users.Login;
using Microsoft.AspNetCore.Authorization;
using SheWolf.Application.Queries.Users.GetById;

namespace SheWolf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getAllUsers")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var getAllUsers = await _mediator.Send(new GetAllUsersQuery());
                return Ok(getAllUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var result = await _mediator.Send(new GetUserByIdQuery(userId));

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


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _mediator.Send(new AddUserCommand(userToAdd));

                if (result.Success)
                {
                    return CreatedAtAction(nameof(GetUserById), new { userId = result.Data.Id }, new { message = result.Message, data = result.Data });
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LogIn([FromBody] User userToLogIn)
        {
            try
            {
                var result = await _mediator.Send(new LogInUserQuery(userToLogIn.Username, userToLogIn.Password));

                if (result.Success)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return Unauthorized(result.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private IActionResult HandleError(Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
        }
    }
}
