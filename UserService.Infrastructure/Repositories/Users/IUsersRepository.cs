using i3rothers.Infrastructure.Repository;
using System;
using System.Threading.Tasks;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Repositories.Users
{
    public interface IUsersRepository: IRepository<User>
    {
        Task<UserWithRoles> GetUserWithRolesByUserNameAsync(string userName);

        Task<UserWithRoles> GetUserWithRolesByUserIdAsync(Guid userId);
    }
}
