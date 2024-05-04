using PlatformService.Models;

namespace PlatformService.Data
{

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
                context.Platforms.AddRange(
                    new Platform() { Name = "Dotnet", Publisher = "Microsoft", Cost = "Free" },
                     new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                     new Platform() { Name = "Kubernetes", Publisher = "Microsoft", Cost = "Free" });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data.");
            }
        }
    }
}