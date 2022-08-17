using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            return service;
        }
    }
}
