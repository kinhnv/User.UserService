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
        public async Task<ApiResult<GetPaginationResult<RoleForList>>> GetRoles([FromQuery] GetRolesParams request)
        {
            var result = await _rolesService.GetRolesAsync(request);
            return result.ToApiResult();
        }

        [HttpGet]
        [Route("SelectOptions")]
        public async Task<ApiResult<IEnumerable<SelectOption>>> GetSelectOptions()
        {
            var result = await _rolesService.GetRolesAsSelectOptionsAsync();
            return result.ToApiResult();
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<RoleForDetails>> GetRole(Guid id)
        {
            var result = await _rolesService.GetRoleAsync(new GetRoleParams { RoleId = id });
            return result.ToApiResult();
        }

        [HttpPost]
        public async Task<ApiResult<RoleForDetails>> PostRole([FromBody] RoleForCreating role)
        {
            var result = await _rolesService.CreateRoleAsync(role);
            return result.ToApiResult();
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<RoleForDetails>> PutRole(Guid id, [FromBody] RoleForEditing role)
        {
            if (id != role.RoleId)
            {
                return new ServiceFailedResult<RoleForDetails>().ToApiResult();
            }

            var result = await _rolesService.EditRoleAsync(role);
            return result.ToApiResult();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteRole(Guid id)
        {
            var result = await _rolesService.DeleteRoleAsync(new DeleteRoleParams { RoleId = id });
            return result.ToApiResult();
        }
    }
}
