using Infrastructure.Persistence.SQLServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.SQLServer.Design
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("designsettings.json")
                .Build();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var designTimeConnectionString = configuration.GetConnectionString("AppDbDesign");
            optionBuilder.UseSqlServer(designTimeConnectionString);
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}
