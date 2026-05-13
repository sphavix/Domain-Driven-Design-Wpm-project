using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Api.Application.Services;
using Wpm.Clinic.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClinicDbContext>(options =>
{
    options.UseSqlite("Data Source=WpmClinic.db");
});

builder.Services.AddScoped<ClinicApplicationService>();

var app = builder.Build();
app.EnsureDbIsCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wpm Clinic Api");
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
