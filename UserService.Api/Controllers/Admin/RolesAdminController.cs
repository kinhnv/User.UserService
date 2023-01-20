using Microsoft.AspNetCore.Mvc;
using TestMaker.Common.Models;
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
            return Ok(new ApiResult<GetPaginationResult<RoleForList>>(result));
        }

        [HttpGet]
        [Route("SelectOptions")]
        public async Task<ActionResult> GetSelectOptions()
        {
            var result = await _rolesService.GetRolesAsSelectOptionsAsync();
            return Ok(new ApiResult<IEnumerable<SelectOption>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRole(Guid id)
        {
            var result = await _rolesService.GetRoleAsync(id);
            return Ok(new ApiResult<RoleForDetails>(result));
        }

        [HttpPost]
        public async Task<ActionResult> PostRole([FromBody] RoleForCreating role)
        {
            var result = await _rolesService.CreateRoleAsync(role);
            return Ok(new ApiResult<RoleForDetails>(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutRole(Guid id, [FromBody] RoleForEditing role)
        {
            if (id != role.RoleId)
            {
                return Ok(new ApiResult());
            }

            var result = await _rolesService.EditRoleAsync(role);
            return Ok(new ApiResult<RoleForDetails>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _rolesService.DeleteRoleAsync(id);
            return Ok(new ApiResult(result));
        }
    }
}
