using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using UserService.Domain.Models;
using UserService.Domain.Models.User;

namespace UserService.Domain.Services
{
    public interface IUsersService
    {
        Task<ServiceResult<GetPaginationResult<UserForList>>> GetUsersAsync(GetUserParams getUserParams);

        Task<ServiceResult<UserForDetails>> GetUserAsync(Guid testId);

        Task<ServiceResult<UserForDetails>> CreateUserAsync(UserForCreating test);

        Task<ServiceResult<UserForDetails>> EditUserAsync(UserForEditing test);

        Task<ServiceResult> DeleteUserAsync(Guid testId);
    }
}
