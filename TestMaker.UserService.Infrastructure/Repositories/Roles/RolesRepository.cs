using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Repositories.Roles;

namespace TestMaker.UserService.Infrastructure.Repositories.Users
{
    public class RolesRepository: Repository<Role>, IRolesRepository
    {
        public RolesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
