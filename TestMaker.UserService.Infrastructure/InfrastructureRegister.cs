using Ddd.Helpers;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Extensions;
using TestMaker.UserService.Domain.Services;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Extensions;
using TestMaker.UserService.Infrastructure.IdentityServer;
using TestMaker.UserService.Infrastructure.Repositories.Roles;
using TestMaker.UserService.Infrastructure.Repositories.RoleUsers;
using TestMaker.UserService.Infrastructure.Repositories.Users;
using TestMaker.UserService.Infrastructure.Services;

namespace TestMaker.UserService.Infrastructure
{
    public class InfrastructureRegister : IInfrastructureRegister
    {
        public void AddInfrastructure(IServiceCollection service, ConfigurationManager configuration, IWebHostEnvironment environment)
        {
            service.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(configuration["Mssql:ConnectionString"]);
            });

            service.AddMongoContext(configuration["Mongodb:ConnectionString"]);

            service.AddAutoMapperProfiles();

            // Repositories
            service.AddTransient<IUsersRepository, UsersRepository>();
            service.AddTransient<IRoleUsersRepository, RoleUsersRepository>();
            service.AddTransient<IRolesRepository, RolesRepository>();

            // Services
            service.AddTransient<IUsersService, UsersService>();
            service.AddTransient<IRolesService, RolesService>();

            // Services For Identity
            service.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            service.AddTransient<IProfileService, ProfileService>();

            service.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddProfileService<ProfileService>();
        }

        public void UseInfrastructure(WebApplication builder)
        {
        }
    }
}
