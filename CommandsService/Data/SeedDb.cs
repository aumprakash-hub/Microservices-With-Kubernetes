using CommandsService.Models;

namespace CommandsService.Data;

public static class SeedDb
{
    public static void PrepPopulation(IApplicationBuilder app,IWebHostEnvironment hostEnvironment)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            SeedData(scope.ServiceProvider.GetService<AppDbContext>(),hostEnvironment);
        }

    }

    private static void SeedData(AppDbContext context, IWebHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsDevelopment())
        {
            //context.Database.Migrate();
        }
            
        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding data...");
            context.Platforms.AddRange(new Platform() { Name = "Sql server", ExternalId = 1 },
                new Platform() { Name = "Docker", ExternalId = 2 });
            context.Commands.AddRange(new Command() { PlatformId = 1, CommandLine = "Fan out", HowTo = "RabbitMQ" },
                new Command() { PlatformId = 2, CommandLine = "Fan out", HowTo = "RabbitMQ message 2" });
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data.");
        }
    }
}