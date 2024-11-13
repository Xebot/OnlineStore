using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DbMigrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
             .AddDbContext<MigrationDbContext>(options =>
                 options.UseNpgsql("Host=45.87.246.64;Database=trainingdb;Username=test;Password=test@12345!"))
             .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();

                Console.WriteLine("Applying migrations...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrations applied successfully.");
            }
        }
    }
}
