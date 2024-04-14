using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
IServiceCollection services = builder.Services;

services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// services.AddSwaggerGen(s =>
// {
//     s.SwaggerDoc("V1", new OpenApiInfo
//     {
//         Title = "PlatformService.Kubernets",
//         Version = "1"
//     });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();