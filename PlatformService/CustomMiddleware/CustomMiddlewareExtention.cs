using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

namespace PlatformService.CustomMiddleware;

public static class CustomMiddlewareExtention
{
    public static WebApplication CustomEnvironmentMiddleware(this WebApplication application,IServiceCollection serviceCollection,IWebHostEnvironment hostEnvironment, IConfiguration configuration)
    {
        if (hostEnvironment.IsDevelopment())
        {
            Console.WriteLine("Sqlserver..");
            serviceCollection.AddDbContext<AppDbContext>((option) => option.UseSqlServer(connectionString:  configuration.GetConnectionString("DbConnectionString") ));
        }
        return application;
    }
}