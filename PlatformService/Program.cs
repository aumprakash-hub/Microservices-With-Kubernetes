using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using PlatformService.CustomMiddleware;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
IServiceCollection services = builder.Services;

//services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
services.AddDbContext<AppDbContext>((option) => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));
services.AddScoped<IPlatformRepo,PlatformRepo>();
services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
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
app.UseHttpsRedirection();
//app.CustomEnvironmentMiddleware(services,app.Environment, new ConfigurationManager());
app.MapControllers();
SeedDb.PrepPopulation(app,app.Environment);
app.Run();