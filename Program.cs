using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(); // Optional, if using Swashbuckle

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi(); // CS1061: MapOpenApi not found
    // Use builder.Services.AddEndpointsApiExplorer() and RegisterSwaggerGen()
    // or map Swagger UI endpoint manually
    // Use AddEndpointsApiExplorer and SwaggerGen instead
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
