using GenerationClient.DataAccesss;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WeightControl.Persistence
{
    public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(options.Options);
        }
    }
}
