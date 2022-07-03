using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.UserService.Domain.Models;
using TestMaker.UserService.Domain.Models.Role;

namespace TestMaker.RoleService.Domain.Services
{
    public interface IRolesService
    {
        Task<ServiceResult<GetPaginationResult<RoleForList>>> GetRolesAsync(GetRolesParams getRolesParams);

        Task<ServiceResult<RoleForDetails>> GetRoleAsync(Guid roleId);

        Task<ServiceResult<RoleForDetails>> CreateRoleAsync(RoleForCreating role);

        Task<ServiceResult<RoleForDetails>> EditRoleAsync(RoleForEditing role);

        Task<ServiceResult> DeleteRoleAsync(Guid roleId);

        Task<ServiceResult<IEnumerable<SelectOption>>> GetRolesAsSelectOptionsAsync();
    }
}
