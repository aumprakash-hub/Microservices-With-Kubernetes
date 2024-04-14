using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
IServiceCollection services = builder.Services;

services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
services.AddScoped<IPlatformRepo,PlatformRepo>();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();
SeedDb.PrepPopulation(app);
app.Run();