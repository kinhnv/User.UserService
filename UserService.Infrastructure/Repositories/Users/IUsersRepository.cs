using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Repositories.Users
{
    public interface IUsersRepository: IRepository<User>
    {
        Task<UserWithRoles?> GetUserWithRolesByUserNameAsync(string userName);

        Task<UserWithRoles?> GetUserWithRolesByUserIdAsync(Guid userId);
    }
}
