using i3rothers.Domain.Models;
using System.Threading.Tasks;
using UserService.Domain.Models.User;

namespace UserService.Domain.Services
{
    public interface IUsersService
    {
        Task<ServiceResult<GetPaginationResult<UserForList>>> GetUsersAsync(GetUsersParams getUserParams);

        Task<ServiceResult<UserForDetails>> GetUserAsync(GetUserParams @params);

        Task<ServiceResult<UserForDetails>> CreateUserAsync(UserForCreating test);

        Task<ServiceResult<UserForDetails>> EditUserAsync(UserForEditing test);

        Task<ServiceResult> DeleteUserAsync(DeleteUserParams @params);
    }
}
