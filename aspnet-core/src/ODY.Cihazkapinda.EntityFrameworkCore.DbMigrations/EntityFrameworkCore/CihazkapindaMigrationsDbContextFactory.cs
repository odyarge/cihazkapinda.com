using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ODY.Cihazkapinda.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class CihazkapindaMigrationsDbContextFactory : IDesignTimeDbContextFactory<CihazkapindaMigrationsDbContext>
    {
        public CihazkapindaMigrationsDbContext CreateDbContext(string[] args)
        {
            CihazkapindaEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<CihazkapindaMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new CihazkapindaMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ODY.Cihazkapinda.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
