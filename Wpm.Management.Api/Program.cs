using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Api.Application.Handlers;
using Wpm.Management.Api.Application.Services;
using Wpm.Management.Api.Infrastructure;
using Wpm.Management.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ManagementDbContext>(options =>
{
    options.UseSqlite("Data source=WpmManagement.db");
});

//builder.Services.AddScoped<IManagementRepository, ManagementRepository>();
builder.Services.AddScoped<IBreedService, Wpm.Management.Api.Infrastructure.BreedService>();
builder.Services.AddScoped<ManagementApplicationService>();
builder.Services.AddScoped<ICommandHandler<SetWeightCommand>, SetWeightCommandHandler>();

var app = builder.Build();

app.EnsureDbIsCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wpm Management Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
