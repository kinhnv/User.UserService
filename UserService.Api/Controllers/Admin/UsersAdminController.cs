using Microsoft.AspNetCore.Mvc;
using TestMaker.Common.Models;
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
        public async Task<ActionResult> GetUsers([FromQuery] GetUserParams request)
        {
            var result = await _usersService.GetUsersAsync(request);
            return Ok(new ApiResult<GetPaginationResult<UserForList>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var result = await _usersService.GetUserAsync(id);
            return Ok(new ApiResult<UserForDetails>(result));
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] UserForCreating user)
        {
            var result = await _usersService.CreateUserAsync(user);
            return Ok(new ApiResult<UserForDetails>(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(Guid id, [FromBody] UserForEditing user)
        {
            if (id != user.UserId)
            {
                return Ok(new ApiResult());
            }

            var result = await _usersService.EditUserAsync(user);
            return Ok(new ApiResult<UserForDetails>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _usersService.DeleteUserAsync(id);
            return Ok(new ApiResult(result));
        }
    }
}
