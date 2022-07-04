using AspNetCore.Environment.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .AddACS();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration["Mssql:ConnectionString"]);
});

builder.Services.AddIdentityServer4();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseIdentityServer();

app.Run();
