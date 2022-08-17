using AspNetCore.Environment.Extensions;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Extensions;
using Ddd.Helpers;
using Serilog;

var builder = WebApplication.CreateBuilder(args)
    .AddACS();

// Add services to the container.
builder.Services.AddControllers();

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add Services and repositories
builder.Services.AddCaching(builder.Configuration);

// Add AddInfrastructure
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

// Add Serilog
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.ReadFrom.Configuration(builder.Configuration);
});

// Add Bearer Authentication
builder.Services.AddBearerAuthentication(builder.Configuration);

// Add ?
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
