using AspNetCore.Environment.Extensions;
using Ddd.Helpers;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddACS();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Add AddInfrastructure
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseIdentityServer();

app.Run();
