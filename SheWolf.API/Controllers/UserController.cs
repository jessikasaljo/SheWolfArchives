using MediatR;
using Microsoft.AspNetCore.Mvc;
using SheWolf.Domain.Entities;
using SheWolf.Application.Queries.Users.GetAll;
using SheWolf.Application.Commands.Users.AddUser;
using SheWolf.Application.Queries.Users.Login;

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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userToAdd)
        {
            try
            {
                var userToBeAdded = await _mediator.Send(new AddUserCommand(userToAdd));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LogIn([FromBody] User userToLogIn)
        {
            try
            {
                var userToBeLoggedIn = await _mediator.Send(new LogInUserQuery(userToLogIn));
                return Ok(userToBeLoggedIn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
