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
        public async Task<ApiResult<GetPaginationResult<UserForList>>> GetUsers([FromQuery] GetUsersParams request)
        {
            var result = await _usersService.GetUsersAsync(request);
            return result.ToApiResult();
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<UserForDetails>> GetUser(Guid id)
        {
            var result = await _usersService.GetUserAsync(new GetUserParams { UserId = id });
            return result.ToApiResult();
        }

        [HttpPost]
        public async Task<ApiResult<UserForDetails>> PostUser([FromBody] UserForCreating user)
        {
            var result = await _usersService.CreateUserAsync(user);
            return result.ToApiResult();
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<UserForDetails>> PutUser(Guid id, [FromBody] UserForEditing user)
        {
            if (id != user.UserId)
            {
                return new ServiceFailedResult<UserForDetails>().ToApiResult();
            }

            var result = await _usersService.EditUserAsync(user);
            return result.ToApiResult();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteUser(Guid id)
        {
            var result = await _usersService.DeleteUserAsync(new DeleteUserParams { UserId = id });
            return result.ToApiResult();
        }
    }
}
