using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Middleware;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Infrastructure.Interfaces;
using StudentManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Custom validation response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                e => e.Key,
                e => e.Value!.Errors.Select(x => x.ErrorMessage).ToArray()
            );

        var response = new
        {
            status = StatusCodes.Status400BadRequest,
            error = "Validation failed",
            details = errors
        };

        return new BadRequestObjectResult(response);
    };
});

// DbContext
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseInMemoryDatabase("StudentDb"));

// DI
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
