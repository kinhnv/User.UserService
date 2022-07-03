using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Repositories.Roles;

namespace TestMaker.UserService.Infrastructure.Repositories.RoleUsers
{
    public class RoleUsersRepository: Repository<RoleUser>, IRoleUsersRepository
    {
        public RoleUsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task RemoveRoleUserByUserIdAsync(Guid userId)
        {
            var roleUsers = _dbContext.Set<RoleUser>().Where(x => x.UserId == userId).ToList();
            _dbContext.Set<RoleUser>().RemoveRange(roleUsers);
            await _dbContext.SaveChangesAsync();
        }
    }
}
