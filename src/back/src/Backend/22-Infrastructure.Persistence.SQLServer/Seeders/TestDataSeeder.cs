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
        IOptions<DataConfiguration> dataConfig
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
            var users = GetMockUsers(DateTimeOffset.Now);

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
                Tuple.Create(
                    new UserDao()
                    {
                        UserName = "admin",
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@pcea.com",
                        PhoneNumber = "01 02 03 04 05",
                        UserType = UserType.ElectionOfficer,
                    },
                    new List<string> { AppConstants.SuperAdminRole }
                ),
                Tuple.Create(
                    new UserDao()
                    {
                        UserName = "user1",
                        FirstName = "Sam",
                        LastName = "Gamegie",
                        PhoneNumber = "01 02 03 04 05",
                        UserType = UserType.Voter,
                        Email = "sam.gamegie@pcea.com",
                    },
                    new List<string> { UserType.Voter.ToString() }
                ),
                Tuple.Create(
                    new UserDao()
                    {
                        UserName = "user2",
                        FirstName = "Bilbo",
                        LastName = "Baggins",
                        PhoneNumber = "01 02 03 04 05",
                        UserType = UserType.Candidate,
                        Email = "bilbo.baggins@pcea.com",
                    },
                    new List<string> { UserType.Voter.ToString(), UserType.Candidate.ToString() }
                ),
                Tuple.Create(
                    new UserDao()
                    {
                        UserName = "user3",
                        FirstName = "Merry",
                        LastName = "Brandybuck",
                        PhoneNumber = "01 02 03 04 05",
                        Email = "merry.brandybuck@pcea.com",
                        UserType = UserType.Candidate,
                    },
                    new List<string> { UserType.Candidate.ToString() }
                ),
                Tuple.Create(
                    new UserDao()
                    {
                        UserName = "user4",
                        FirstName = "Arwen",
                        LastName = "Evenstar",
                        PhoneNumber = "01 02 03 04 05",
                        Email = "arwen.evenstar@pcea.com",
                        UserType = UserType.ElectionOfficer,
                    },
                    new List<string>
                    {
                        UserType.ElectionOfficer.ToString(),
                        UserType.Voter.ToString(),
                    }
                ),
                Tuple.Create(
                    new UserDao()
                    {
                        UserName = "user5",
                        FirstName = "Harvey",
                        LastName = "Spector",
                        PhoneNumber = "06 02 03 04 05",
                        Email = "harvey.spector@pcea.com",
                        UserType = UserType.ElectionOfficer,
                    },
                    new List<string>
                    {
                        UserType.ElectionOfficer.ToString(),
                        UserType.Voter.ToString(),
                    }
                ),
                Tuple.Create(
                    new UserDao()
                    {
                        UserName = "user6",
                        FirstName = "Julie",
                        LastName = "Dupuy",
                        PhoneNumber = "07 02 03 04 05",
                        Email = "julie.dupuy@pcea.com",
                        UserType = UserType.ElectionOfficer,
                        DisabledDate = now.AddDays(-1),
                    },
                    new List<string>
                    {
                        UserType.ElectionOfficer.ToString(),
                        UserType.Voter.ToString(),
                    }
                ),
            };

            return users;
        }
    }
}
