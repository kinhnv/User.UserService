using i3rothers.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Services;
using Xunit;

namespace UserService.UnitTests.Services
{
    public class UsersServiceTest : BaseTest<IUsersService>
    {
        [Fact]
        public async Task GetUserAsync_ErrorValidate()
        {
            Assert.True(true);
        }
    }
}
