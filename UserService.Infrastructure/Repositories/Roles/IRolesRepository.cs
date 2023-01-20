using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Repositories.Roles
{
    public interface IRolesRepository: IRepository<Role>
    {
    }
}
