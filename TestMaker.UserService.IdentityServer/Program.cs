using AspNetCore.Environment.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .AddACS();

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration["Mssql:ConnectionString"]);
});

builder.Services.AddIdentityServer4();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
