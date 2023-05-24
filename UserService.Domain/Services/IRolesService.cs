using i3rothers.Domain.Models;
using UserService.Domain.Models;
using UserService.Domain.Models.Role;

namespace UserService.Domain.Services
{
    public interface IRolesService
    {
        Task<ServiceResult<GetPaginationResult<RoleForList>>> GetRolesAsync(GetRolesParams getRolesParams);

        Task<ServiceResult<RoleForDetails>> GetRoleAsync(GetRoleParams @params);

        Task<ServiceResult<RoleForDetails>> CreateRoleAsync(RoleForCreating role);

        Task<ServiceResult<RoleForDetails>> EditRoleAsync(RoleForEditing role);

        Task<ServiceResult> DeleteRoleAsync(DeleteRoleParams roleId);

        Task<ServiceResult<IEnumerable<SelectOption>>> GetRolesAsSelectOptionsAsync();
    }
}
