using Microsoft.EntityFrameworkCore;

namespace DemoAutoMigration.Models
{
    public class JobContext : DbContext
    {
        public DbSet<Job> jobs { get; set; }
        public DbSet<Category> categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = config.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("JobConstr"));
        }
    }
}
