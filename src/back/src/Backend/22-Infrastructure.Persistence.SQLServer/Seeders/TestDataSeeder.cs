using Application.Common.Enums;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tools.Constants;

namespace Infrastructure.Persistence.SQLServer.Seeders
{
    public class TestDataSeeder(
        WritableDbContext context,
        UserManager<UserDao> userManager,
        IOptions<DataConfiguration> dataConfig,
        TimeProvider timeProvider
    ) : SeederBase(context, userManager)
    {
        public override async Task SeedDataAsync()
        {
            if (!dataConfig.Value.SeedTest)
            {
                return;
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteInTransactionAsync(
                async () =>
                {
                    //await SeedStakeholdersAsync();
                    //await SeedGeoZonesAsync();
                    await SeedUsersAsync();
                },
                () => Task.FromResult(true)
            );
        }

        private async Task SeedUsersAsync()
        {
            var users = GetMockUsers(timeProvider.GetUtcNow());

            foreach (var user in users)
            {
                if (!(await _context.Users.AnyAsync(u => u.UserName == user.Item1.UserName)))
                    await SeedUserAsync(user.Item1, "Secret01", user.Item2);
            }

            await _context.SaveChangesAsync();
        }

        public static IEnumerable<Tuple<UserDao, List<string>>> GetMockUsers(DateTimeOffset now)
        {
            var users = new List<Tuple<UserDao, List<string>>>
            {
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mr,
                        UserName = "admin",
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@pcea.com",
                        PhoneNumber = "01 02 03 04 05",
                    },
                    [AppConstants.SuperAdminRole]
                ),
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mr,
                        UserName = "user1",
                        FirstName = "Sam",
                        LastName = "Gamegie",
                        PhoneNumber = "01 02 03 04 05",
                        Email = "sam.gamegie@pcea.com",
                    },
                    [AppConstants.SupervisorRole]
                ),
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mr,
                        UserName = "user2",
                        FirstName = "Bilbo",
                        LastName = "Baggins",
                        PhoneNumber = "01 02 03 04 05",
                        Email = "bilbo.baggins@pcea.com",
                    },
                    [AppConstants.ElectorRole]
                ),
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mrs,
                        UserName = "user3",
                        FirstName = "Merry",
                        LastName = "Brandybuck",
                        PhoneNumber = "01 02 03 04 05",
                        Email = "merry.brandybuck@pcea.com",
                    },
                    [AppConstants.CommissionChairmanRole]
                ),
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mr,
                        UserName = "user4",
                        FirstName = "Arwen",
                        LastName = "Evenstar",
                        PhoneNumber = "01 02 03 04 05",
                        Email = "arwen.evenstar@pcea.com",
                    },
                    [AppConstants.DataEntryOperatorRole]
                ),
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mr,
                        UserName = "user5",
                        FirstName = "Harvey",
                        LastName = "Spector",
                        PhoneNumber = "06 02 03 04 05",
                        Email = "harvey.spector@pcea.com",
                    },
                    [AppConstants.ElectorRole]
                ),
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mrs,
                        UserName = "user6",
                        FirstName = "Julie",
                        LastName = "Dupuy",
                        PhoneNumber = "07 02 03 04 05",
                        Email = "julie.dupuy@pcea.com",
                        DisabledDate = now.AddDays(-1),
                    },
                    [AppConstants.ElectorRole]
                ),
                Tuple.Create<UserDao, List<string>>(
                    new()
                    {
                        Title = PersonTitle.Mr,
                        UserName = "user7",
                        FirstName = "Bolan",
                        LastName = "Marck",
                        PhoneNumber = "07 02 08 04 05",
                        Email = "bolan.marck@pcea.com",
                        DisabledDate = now.AddDays(-1),
                    },
                    [AppConstants.ElectorRole]
                ),
            };

            return users;
        }
    }
}
