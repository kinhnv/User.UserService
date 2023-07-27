using AutoMapper;
using i3rothers.Domain.Extensions;
using i3rothers.Domain.Models;
using i3rothers.Infrastructure.Repository;
using i3rothers.UnitTests;
using Moq;
using System.Linq.Expressions;
using UserService.Domain.Models.Role;
using UserService.Domain.Services;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Extensions;
using UserService.Infrastructure.Repositories.Roles;
using UserService.Infrastructure.Services;
using Xunit;

namespace UserService.UnitTests.Services
{
    public class RolesServiceTest : BaseTest<IRolesService>
    {
        private readonly Role _role;
        private readonly RoleForDetails _roleForDetails;

        public RolesServiceTest()
        {
            AddObject(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper());

            _role = new Role
            {
                Name = "Admin",
                RoleId = Guid.NewGuid()
            };

            _roleForDetails = new RoleForDetails
            {
                Name = _role.Name,
                RoleId = _role.RoleId
            };
        }

        public BaseMock<IRolesRepository> MockRolesRepository
        {
            get
            {
                return Mock<IRolesRepository>();
            }
        }

        [Theory]
        [InlineData("", "Name: BLANK")]
        [InlineData(null, "Name: BLANK")]
        [InlineData("0123456789012345678901234567890123456789012345678901234567890123456789", "Name: LONGER THAN 64")]
        public async Task CreateRoleAsync_ErrorValidate(string name, string message)
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.CreateAsync(It.IsAny<Role>()), Times.Never);

                MockRolesRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Role, bool>>>()), Times.Never);
            });

            var result = await service.CreateRoleAsync(new RoleForCreating
            {
                Name = name,
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult<RoleForDetails>>(result);
            Assert.Equal(message, ((ServiceFailedResult<RoleForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task CreateRoleAsync_Success()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.CreateAsync(It.Is<Role>(r => r.Name == _role.Name)), Times.Once)
                    .Callback<Role>(role =>
                    {
                        role.RoleId = _role.RoleId;
                    });

                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Once)
                    .ReturnsAsync(new List<Role> { _role });
            });

            var result = await service.CreateRoleAsync(new RoleForCreating
            {
                Name = _role.Name,
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult<RoleForDetails>>(result);
            Assert.Equal(_roleForDetails.ToJson(), ((ServiceSuccessfulResult<RoleForDetails>)result)?.Data?.ToJson());
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "RoleId: BLANK")]
        public async Task DeleteRoleAsync_ErrorValidate(string roleId, string message)
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Role, bool>>>()), Times.Never);
                MockRolesRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<Role, bool>>>()), Times.Never);
            });

            var result = await service.DeleteRoleAsync(new DeleteRoleParams
            {
                RoleId = Guid.Parse(roleId),
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult>(result);
            Assert.Equal(message, ((ServiceFailedResult)result).ErrorMessages.First());
        }

        [Fact]
        public async Task DeleteRoleAsync_NotFound()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Once)
                    .ReturnsAsync(new List<Role> ());
                MockRolesRepository
                    .Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<Role, bool>>>()), Times.Never);
            });

            var result = await service.DeleteRoleAsync(new DeleteRoleParams
            {
                RoleId = _role.RoleId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceNotFoundResult>(result);
            Assert.Equal(new ServiceNotFoundResult(typeof(Role), _role.RoleId).ErrorMessages.First(), ((ServiceNotFoundResult)result).ErrorMessages.First());
        }

        [Fact]
        public async Task DeleteRoleAsync_Success()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Once)
                    .ReturnsAsync(new List<Role> { _role });
                MockRolesRepository
                    .Setup(x => x.DeleteAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Once);
            });

            var result = await service.DeleteRoleAsync(new DeleteRoleParams
            {
                RoleId = _role.RoleId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult>(result);
        }


        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "name", "RoleId: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000001", "", "Name: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000001", null, "Name: BLANK")]
        [InlineData("00000000-0000-0000-0000-000000000001", "0123456789012345678901234567890123456789012345678901234567890123456789", "Name: LONGER THAN 64")]
        public async Task EditRoleAsync_ErrorValidate(string roleId, string name, string message)
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.UpdateAsync(It.IsAny<Role>()), Times.Never);

                MockRolesRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Role, bool>>>()), Times.Never);
            });

            var result = await service.EditRoleAsync(new RoleForEditing
            {
                RoleId = Guid.Parse(roleId),
                Name = name,
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult<RoleForDetails>>(result);
            Assert.Equal(message, ((ServiceFailedResult<RoleForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task EditRoleAsync_NotFound()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Once)
                    .ReturnsAsync(new List<Role> ());

                MockRolesRepository
                    .Setup(x => x.UpdateAsync(It.Is<Role>(r => r.RoleId == _role.RoleId)), Times.Never);
            });

            var result = await service.EditRoleAsync(new RoleForEditing
            {
                RoleId = _role.RoleId,
                Name = _role.Name,
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceNotFoundResult<RoleForDetails>>(result);
            Assert.Equal(new ServiceNotFoundResult<RoleForDetails>(_role.RoleId).ErrorMessages.First(), ((ServiceNotFoundResult<RoleForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task EditRoleAsync_Success()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.UpdateAsync(It.Is<Role>(r => r.RoleId == _role.RoleId)), Times.Once);

                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Exactly(2))
                    .ReturnsAsync(new List<Role> { _role });
            });

            var result = await service.EditRoleAsync(new RoleForEditing
            {
                RoleId = _role.RoleId,
                Name = _role.Name,
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult<RoleForDetails>>(result);
            Assert.Equal(_roleForDetails.ToJson(), ((ServiceSuccessfulResult<RoleForDetails>)result)?.Data?.ToJson());
        }


        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "RoleId: BLANK")]
        public async Task GetRoleAsync_ErrorValidate(string roleId, string message)
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Role, bool>>>()), Times.Never);
            });

            var result = await service.GetRoleAsync(new GetRoleParams
            {
                RoleId = Guid.Parse(roleId)
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceFailedResult<RoleForDetails>>(result);
            Assert.Equal(message, ((ServiceFailedResult<RoleForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task GetRoleAsync_NotFound()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Once)
                    .ReturnsAsync(new List<Role>());
            });

            var result = await service.GetRoleAsync(new GetRoleParams
            {
                RoleId = _role.RoleId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceNotFoundResult<RoleForDetails>>(result);
            Assert.Equal(new ServiceNotFoundResult<RoleForDetails>(_role.RoleId).ErrorMessages.First(), ((ServiceNotFoundResult<RoleForDetails>)result).ErrorMessages.First());
        }

        [Fact]
        public async Task GetRoleAsync_Success()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<Expression<Func<Role, bool>>>(e => e.Compile().Invoke(_role))), Times.Once)
                    .ReturnsAsync(new List<Role> { _role });
            });

            var result = await service.GetRoleAsync(new GetRoleParams
            {
                RoleId = _role.RoleId
            });

            VerifyMockedMethods();

            Assert.IsType<ServiceSuccessfulResult<RoleForDetails>>(result);
            Assert.Equal(_roleForDetails.ToJson(), ((ServiceSuccessfulResult<RoleForDetails>)result)?.Data?.ToJson());
        }

        [Fact]
        public async Task GetRolesAsync_Success()
        {
            var service = CreateInstance<RolesService>(() =>
            {
                MockRolesRepository
                    .Setup(x => x.GetAsync(It.Is<GetParams<Role>>(p => p.Skip == 0 && p.Take == 10)), Times.Once)
                    .ReturnsAsync(new List<Role> { _role });
                MockRolesRepository
                    .Setup(x => x.CountAsync(It.IsAny<GetParams<Role>>()), Times.Once)
                    .ReturnsAsync(1);
            });

            var result = await service.GetRolesAsync(new GetRolesParams
            {
                Page = 1,
                Take = 10
            });

            VerifyMockedMethods();

            var getPaginationResult = new GetPaginationResult<RoleForList>
            {
                Data = new List<RoleForList> { new RoleForList { RoleId = _role.RoleId, Name = _role.Name } },
                Page = 1,
                Take = 10,
                TotalRecord = 1
            };

            Assert.IsType<ServiceSuccessfulResult<GetPaginationResult<RoleForList>>>(result);
            Assert.Equal(getPaginationResult.ToJson(), ((ServiceSuccessfulResult<GetPaginationResult<RoleForList>>)result)?.Data?.ToJson());
        }
    }
}
