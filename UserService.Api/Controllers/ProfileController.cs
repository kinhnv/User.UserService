using i3rothers.Domain.Extensions;
using i3rothers.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Models.User;
using UserService.Domain.Services;

namespace UserService.Api.Controllers
{
    [Authorize]
    [Route("api/Profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IUsersService _usersService;

        public ProfileController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userId = User.GetUserId();
            var result = await _usersService.GetUserAsync(new GetUserParams { UserId = userId ?? Guid.Empty });
            return Ok(result.ToApiResult());
        }
    }
}
