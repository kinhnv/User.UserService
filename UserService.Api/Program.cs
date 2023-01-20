using AspNetCore.Environment.Extensions;
using i3rothers.Domain.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

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

// Add Serilog
builder.Host.UseSerilog((hostContext, services, configuration) => {
    var url = builder.Configuration.GetConfiguration("ElasticSearch:Url");
    if (!string.IsNullOrEmpty(url))
    {
        configuration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(url))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{builder.Environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        });
    }
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
