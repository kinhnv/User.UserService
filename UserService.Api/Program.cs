using i3rothers.AspNetCore.Extensions;
using i3rothers.Domain.Extensions;

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

// Add AddInfrastructure
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

// Add Bearer Authentication
builder.Services.AddAuthenticationByIdentityServer(builder.Configuration);

// Add ?
builder.Services.AddSwaggerDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
