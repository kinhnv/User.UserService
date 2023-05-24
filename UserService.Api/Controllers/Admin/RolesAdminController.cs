using i3rothers.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Models;
using UserService.Domain.Models.Role;
using UserService.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Api.Controllers.Admin
{
    [Route("api/Admin/Roles")]
    [ApiController]
    public class RolesAdminController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesAdminController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRoles([FromQuery] GetRolesParams request)
        {
            var result = await _rolesService.GetRolesAsync(request);
            return Ok(result.ToApiResult());
        }

        [HttpGet]
        [Route("SelectOptions")]
        public async Task<ActionResult> GetSelectOptions()
        {
            var result = await _rolesService.GetRolesAsSelectOptionsAsync();
            return Ok(result.ToApiResult());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRole(Guid id)
        {
            var result = await _rolesService.GetRoleAsync(new GetRoleParams { RoleId = id });
            return Ok(result.ToApiResult());
        }

        [HttpPost]
        public async Task<ActionResult> PostRole([FromBody] RoleForCreating role)
        {
            var result = await _rolesService.CreateRoleAsync(role);
            return Ok(result.ToApiResult());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutRole(Guid id, [FromBody] RoleForEditing role)
        {
            if (id != role.RoleId)
            {
                return Ok(new ServiceFailedResult().ToApiResult());
            }

            var result = await _rolesService.EditRoleAsync(role);
            return Ok(result.ToApiResult());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _rolesService.DeleteRoleAsync(new DeleteRoleParams { RoleId = id });
            return Ok(result.ToApiResult());
        }
    }
}
