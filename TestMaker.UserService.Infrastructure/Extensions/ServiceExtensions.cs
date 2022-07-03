using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.RoleService.Domain.Services;
using TestMaker.UserService.Domain.Services;
using TestMaker.UserService.Infrastructure.IdentityServer;
using TestMaker.UserService.Infrastructure.Repositories.Roles;
using TestMaker.UserService.Infrastructure.Repositories.RoleUsers;
using TestMaker.UserService.Infrastructure.Repositories.Users;
using TestMaker.UserService.Infrastructure.Services;

namespace TestMaker.UserService.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTransient(this IServiceCollection service)
        {
            service.AddAutoMapperProfiles();

            // Repositories
            service.AddTransient<IUsersRepository, UsersRepository>();
            service.AddTransient<IRoleUsersRepository, RoleUsersRepository>();
            service.AddTransient<IRolesRepository, RolesRepository>();

            // Services
            service.AddTransient<IUsersService, UsersService>();
            service.AddTransient<IRolesService, RolesService>();

            return service;
        }
        public static IServiceCollection AddIdentityServer4(this IServiceCollection service)
        {
            // Repositories
            service.AddTransient<IUsersRepository, UsersRepository>();
            service.AddTransient<IRoleUsersRepository, RoleUsersRepository>();
            service.AddTransient<IRolesRepository, RolesRepository>();

            // Services
            service.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            service.AddTransient<IProfileService, ProfileService>();

            service.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddProfileService<ProfileService>();

            return service;
        }
    }
}
