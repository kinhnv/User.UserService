using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.UserService.Infrastructure.Entities;

namespace TestMaker.UserService.Infrastructure.Repositories.RoleUsers
{
    public interface IRoleUsersRepository: IRepository<UserRole>
    {
        Task RemoveRoleUserByUserIdAsync(Guid userId);
    }
}
