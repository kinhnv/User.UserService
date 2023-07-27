using AutoMapper;
using i3rothers.Domain.Extensions;
using i3rothers.Domain.Models;
using i3rothers.Infrastructure.Repository;
using i3rothers.UnitTests;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Models.User;
using UserService.Domain.Services;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Extensions;
using UserService.Infrastructure.Repositories.Roles;
using UserService.Infrastructure.Repositories.RoleUsers;
using UserService.Infrastructure.Repositories.Users;
using UserService.Infrastructure.Services;
using Xunit;

namespace UserService.UnitTests.Services
{
    public class UsersServiceTest : BaseTest<IUsersService>
    {
        private readonly User _user;
        private readonly Role _role;
        private readonly UserRole _userRole;

        public UsersServiceTest()
        {
            AddObject(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper());

            _user = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "Password",
                UserId = Guid.NewGuid(),
                UserName = "UserName",
            };

            _role = new Role
            {
                Name = "Admin",
                RoleId = Guid.NewGuid()
            };

            _userRole = new UserRole
            {
                UserId = _user.UserId,
                RoleId = _role.RoleId,
            };
        }

        public BaseMock<IUsersRepository> MockUsersRepository
        {
            get
            {
                return Mock<IUsersRepository>();
            }
        }

        public BaseMock<IRolesRepository> MockRolesRepository
        {
            get
            {
                return Mock<IRolesRepository>();
            }
        }

        public BaseMock<IRoleUsersRepository> MockRoleUsersRepository
        {
            get
            {
                return Mock<IRoleUsersRepository>();
            }
        }

        [Theory]
        [InlineData("", "123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "UserName: BLANK")]
        [InlineData(null, "123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "UserName: BLANK")]
        [InlineData("0123456789012345678901234567890123456789012345678901234567890123456789", "123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "UserName: LONGER THAN 64")]
        [InlineData("userName", "", "[\"00000000-0000-0000-0000-000000000001\"]", "Password: BLANK")]
        [InlineData("userName", null, "[\"00000000-0000-0000-0000-000000000001\"]", "Password: BLANK")]
        [InlineData("userName", "0123456789012345678901234567890123456789012345678901234567890123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "Password: LONGER THAN 64")]
        [InlineData("userName", "123456789", null, "RoleIds: BLANK")]
        public async Task CreateUserAsync_ErrorValidate(string userName, string password, string roleIds, string message)
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.CreateAsync(It.IsAny<User>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.CreateAsync(It.IsAny<List<UserRole>>()), Times.Never);
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Never);
            });

            var result = await service.CreateUserAsync(new Domain.Models.User.UserForCreating
            {
                UserName = userName,
                Password = password,
                RoleIds = JsonConvert.DeserializeObject<List<Guid>>(roleIds ?? "[]") ?? new List<Guid>()
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult<UserForDetails>>(result);
            Assert.Equal(message, ((ServiceFailedResult<UserForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task CreateUserAsync_Success()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.CreateAsync(It.Is<User>(u => u.UserName == _user.UserName && u.Password == _user.Password)), Times.Once)
                    .Callback<User>(user =>
                    {
                        user.UserId = _user.UserId;
                    });
                MockRoleUsersRepository
                    .Setup(x => x.CreateAsync(It.Is<List<UserRole>>(ur => ur[0].UserId == _user.UserId)), Times.Once);
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(new List<User> { _user });
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<UserRole, bool>>>(e => e.Compile().Invoke(_userRole))), Times.Once)
                    .ReturnsAsync(new List<UserRole> { _userRole });
            });

            var result = await service.CreateUserAsync(new UserForCreating
            {
                UserName = _user.UserName,
                Password = _user.Password,
                RoleIds = new List<Guid> { _role.RoleId }
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult<UserForDetails>>(result);
            Assert.Equal(new UserForDetails
            {
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                RoleIds = new List<Guid> { _userRole.RoleId },
                UserId = _user.UserId,
                UserName = _user.UserName
            }.ToJson(), ((ServiceSuccessfulResult<UserForDetails>)result)?.Data?.ToJson());
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "UserId: BLANK")]
        public async Task DeleteUserAsync_ErrorValidate(string userId, string message)
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
                MockUsersRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Never);
            });

            var result = await service.DeleteUserAsync(new DeleteUserParams
            {
                UserId = Guid.Parse(userId)
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult>(result);
            Assert.Equal(message, ((ServiceFailedResult)result).ErrorMessages.First());
        }

        [Fact]
        public async Task DeleteUserAsync_NotFound()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(new List<User> ());
                MockUsersRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Never);
            });

            var result = await service.DeleteUserAsync(new DeleteUserParams
            {
                UserId = _user.UserId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceNotFoundResult>(result);
            Assert.Equal(new ServiceNotFoundResult(typeof(User), _user.UserId).ErrorMessages.First(), ((ServiceNotFoundResult)result).ErrorMessages.First());
        }

        [Fact]
        public async Task DeleteUserAsync_Success()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(new List<User> { _user });
                MockUsersRepository
                    .Setup(x => x.DeleteAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once);
                MockRoleUsersRepository
                    .Setup(x => x.DeleteAsync(It.Is<Expression<Func<UserRole, bool>>>(e => e.Compile().Invoke(_userRole))), Times.Once);
            });

            var result = await service.DeleteUserAsync(new DeleteUserParams
            {
                UserId = _user.UserId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult>(result);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "userName", "123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "UserId: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000005", "", "123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "UserName: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000005", null, "123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "UserName: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000005", "0123456789012345678901234567890123456789012345678901234567890123456789", "123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "UserName: LONGER THAN 64")]
        [InlineData("00000000-0000-0000-0000-000000000005", "userName", "", "[\"00000000-0000-0000-0000-000000000001\"]", "Password: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000005", "userName", null, "[\"00000000-0000-0000-0000-000000000001\"]", "Password: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000005", "userName", "0123456789012345678901234567890123456789012345678901234567890123456789", "[\"00000000-0000-0000-0000-000000000001\"]", "Password: LONGER THAN 64")]
        [InlineData("00000000-0000-0000-0000-000000000005", "userName", "123456789", null, "RoleIds: BLANK")]
        public async Task EditUserAsync_ErrorValidate(string userId, string userName, string password, string roleIds, string message)
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.CountAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
                MockUsersRepository
                    .Setup(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.CreateAsync(It.IsAny<List<UserRole>>()), Times.Never);
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Never);
            });

            var result = await service.EditUserAsync(new UserForEditing
            {
                UserId = Guid.Parse(userId),
                UserName = userName,
                Password = password,
                RoleIds = JsonConvert.DeserializeObject<List<Guid>>(roleIds ?? "[]") ?? new List<Guid>()
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult<UserForDetails>>(result);
            Assert.Equal(message, ((ServiceFailedResult<UserForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task EditUserAsync_NotFound()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.CountAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(0);
                MockUsersRepository
                    .Setup(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.CreateAsync(It.IsAny<List<UserRole>>()), Times.Never);
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Never);
            });

            var result = await service.EditUserAsync(new UserForEditing
            {
                UserId = _user.UserId,
                UserName = _user.UserName,
                Password = _user.Password,
                RoleIds = new List<Guid> { _role.RoleId }
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceNotFoundResult<UserForDetails>>(result);
            Assert.Equal(new ServiceNotFoundResult<UserForDetails>(_user.UserId).ErrorMessages.First(), ((ServiceNotFoundResult<UserForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task EditUserAsync_Success()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.CountAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(1);
                MockUsersRepository
                    .Setup(x => x.UpdateAsync(It.Is<User>(u => u.UserId == _user.UserId)), Times.Once);
                MockRoleUsersRepository
                    .Setup(x => x.DeleteAsync(It.Is<Expression<Func<UserRole, bool>>>(e => e.Compile().Invoke(_userRole))), Times.Once);
                MockRoleUsersRepository
                    .Setup(x => x.CreateAsync(It.Is<List<UserRole>>(ur => ur.Any(x => x.UserId == _user.UserId))), Times.Once);
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(new List<User> { _user });
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<UserRole, bool>>>(e => e.Compile().Invoke(_userRole))), Times.Once)
                    .ReturnsAsync(new List<UserRole> { _userRole });
            });

            var result = await service.EditUserAsync(new UserForEditing
            {
                UserId = _user.UserId,
                UserName = _user.UserName,
                Password = _user.Password,
                RoleIds = new List<Guid> { _role.RoleId }
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult<UserForDetails>>(result);
            Assert.Equal(new UserForDetails
            {
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                RoleIds = new List<Guid> { _userRole.RoleId },
                UserId = _user.UserId,
                UserName = _user.UserName
            }.ToJson(), ((ServiceSuccessfulResult<UserForDetails>)result)?.Data?.ToJson());
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "UserId: BLANK")]
        public async Task GetUserAsync_ErrorValidate(string userId, string message)
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Never);
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<UserRole, bool>>>(e => e.Compile().Invoke(_userRole))), Times.Never);
            });

            var result = await service.GetUserAsync(new GetUserParams
            {
                UserId = Guid.Parse(userId)
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult<UserForDetails>>(result);
            Assert.Equal(message, ((ServiceFailedResult<UserForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task GetUserAsync_NotFound()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(new List<User> ());
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<UserRole, bool>>>(e => e.Compile().Invoke(_userRole))), Times.Never);
            });

            var result = await service.GetUserAsync(new GetUserParams
            {
                UserId = _user.UserId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceNotFoundResult<UserForDetails>>(result);
            Assert.Equal(new ServiceNotFoundResult<UserForDetails>(_user.UserId).ErrorMessages.First(), ((ServiceNotFoundResult<UserForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task GetUserAsync_Success()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<User, bool>>>(e => e.Compile().Invoke(_user))), Times.Once)
                    .ReturnsAsync(new List<User> { _user });
                MockRoleUsersRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<UserRole, bool>>>(e => e.Compile().Invoke(_userRole))), Times.Once)
                    .ReturnsAsync(new List<UserRole> { _userRole });
            });

            var result = await service.GetUserAsync(new GetUserParams
            {
                UserId = _user.UserId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult<UserForDetails>>(result);
            Assert.Equal(new UserForDetails
            {
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                RoleIds = new List<Guid> { _userRole.RoleId },
                UserId = _user.UserId,
                UserName = _user.UserName
            }.ToJson(), ((ServiceSuccessfulResult<UserForDetails>)result)?.Data?.ToJson());
        }

        [Fact]
        public async Task GetUsersAsync_Success()
        {
            var service = CreateInstance<UsersService>(() =>
            {
                MockUsersRepository
                    .Setup(x => x.GetAsync(It.Is<GetParams<User>>(p => p.Skip == 0 && p.Take == 10)), Times.Once)
                    .ReturnsAsync(new List<User> { _user });
                MockUsersRepository
                    .Setup(x => x.CountAsync(It.IsAny<GetParams<User>>()), Times.Once)
                    .ReturnsAsync(1);
            });

            var result = await service.GetUsersAsync(new GetUsersParams
            {
                Page = 1,
                Take = 10
            });

            VerifyMockedMethods();

            var getPaginationResult = new GetPaginationResult<UserForList>
            {
                Data = new List<UserForList> { new UserForList { UserId = _user.UserId, UserName = _user.UserName } },
                Page = 1,
                Take = 10,
                TotalRecord = 1
            };

            Assert.IsType<ServiceSuccessfulResult<GetPaginationResult<UserForList>>>(result);
            Assert.Equal(getPaginationResult.ToJson(), ((ServiceSuccessfulResult<GetPaginationResult<UserForList>>)result)?.Data?.ToJson());
        }
    }
}
