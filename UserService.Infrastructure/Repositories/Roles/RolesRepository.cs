using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Repositories.Roles;

namespace UserService.Infrastructure.Repositories.Users
{
    public class RolesRepository: Repository<Role>, IRolesRepository
    {
        public RolesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
