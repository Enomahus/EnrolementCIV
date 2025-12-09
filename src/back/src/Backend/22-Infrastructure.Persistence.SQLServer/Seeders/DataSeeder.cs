using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tools.Configuration;
using Tools.Constants;
using Tools.Exceptions;

namespace Infrastructure.Persistence.SQLServer.Seeders
{
    public class DataSeeder(
        WritableDbContext context,
        UserManager<UserDao> userManager,
        RoleManager<RoleDao> roleManager,
        IOptions<DataConfiguration> dataConfig,
        IOptions<AppConfiguration> appConfig
    ) : SeederBase(context, userManager)
    {
        private static readonly string AdminUserName = "pcea_admin";

        public override async Task SeedDataAsync()
        {
            if (!dataConfig.Value.Seed)
            {
                return;
            }
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteInTransactionAsync(
                async () =>
                {
                    //await SeedRolesAsync();
                    //await SeedStakeholdersAsync();
                    await SeedDefaultUserAsync();
                },
                () => Task.FromResult(true)
            );
        }

        private async Task SeedDefaultUserAsync()
        {
            if (dataConfig.Value.DefaultUserPassword is null)
            {
                throw new ConfigurationMissingException(
                    "Missing configuration : DataConfig.DefaultUserConfig"
                );
            }

            await SeedUserAsync(
                AdminUserName,
                "Pcea",
                "Admin",
                "dev@pcea.com",
                "01 02 03 04 05",
                dataConfig.Value.DefaultUserPassword,
                [AppConstants.SuperAdminRole]
            );
        }
    }
}
