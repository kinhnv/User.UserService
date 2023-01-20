using AspNetCore.Environment.Extensions;
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
using UserService.Domain.Services;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Extensions;
using UserService.Infrastructure.IdentityServer;
using UserService.Infrastructure.Repositories.Roles;
using UserService.Infrastructure.Repositories.RoleUsers;
using UserService.Infrastructure.Repositories.Users;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure
{
    public class InfrastructureRegister : IInfrastructureRegister
    {
        public void AddInfrastructure(IServiceCollection service, ConfigurationManager configuration, IWebHostEnvironment environment)
        {
            service.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConfiguration("Mssql:ConnectionString"));
            });

            service.AddMongoContext(new Common.Mongodb.MongoDbSettings
            {
                ConnectionString = configuration.GetConfiguration("Mongodb:ConnectionString"),
                Database = configuration.GetConfiguration("Mongodb:Database")
            });

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
