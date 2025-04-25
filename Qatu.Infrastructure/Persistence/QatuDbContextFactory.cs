using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Qatu.Infrastructure.Persistence
{
    public class QatuDbContextFactory : IDesignTimeDbContextFactory<QatuDbContext>
    {
        public QatuDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Qatu.API"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultDevConnection");

            var optionsBuilder = new DbContextOptionsBuilder<QatuDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new QatuDbContext(optionsBuilder.Options);
        }
    }
}
