using i3rothers.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Models.User;
using UserService.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Api.Controllers.Admin
{
    [Route("api/Admin/Users")]
    [ApiController]
    public class UsersAdminController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersAdminController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        public async Task<ActionResult> GetUsers([FromQuery] GetUsersParams request)
        {
            var result = await _usersService.GetUsersAsync(request);
            return Ok(result.ToApiResult());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var result = await _usersService.GetUserAsync(new GetUserParams { UserId = id });
            return Ok(result.ToApiResult());
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] UserForCreating user)
        {
            var result = await _usersService.CreateUserAsync(user);
            return Ok(result.ToApiResult());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(Guid id, [FromBody] UserForEditing user)
        {
            if (id != user.UserId)
            {
                return Ok(new ServiceFailedResult().ToApiResult());
            }

            var result = await _usersService.EditUserAsync(user);
            return Ok(result.ToApiResult());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _usersService.DeleteUserAsync(new DeleteUserParams { UserId = id });
            return Ok(result.ToApiResult());
        }
    }
}
