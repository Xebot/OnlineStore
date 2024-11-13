using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMigrator
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
    {
        public MigrationDbContext CreateDbContext(string[] args)
        {
            

            // Получение строки подключения из конфигурации
            var connectionString = "Host=45.87.246.64;Database=trainingdb;Username=test;Password=test@12345!";

            var optionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
            optionsBuilder.UseNpgsql(connectionString); // Настройте вашу БД

            return new MigrationDbContext(optionsBuilder.Options);
        }
    }
}
