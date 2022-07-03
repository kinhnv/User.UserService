using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.UserService.Infrastructure.Entities;

namespace TestMaker.UserService.Infrastructure.Repositories.Users
{
    public class UsersRepository: Repository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
