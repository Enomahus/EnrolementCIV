using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Common
{
    public class UserCommandHandlerBase(
        WritableDbContext context,
        UserManager<UserDao> userManager,
        TimeProvider timeProvider
    )
    {
        protected readonly UserManager<UserDao> _userManager = userManager;
        protected readonly TimeProvider _timeProvider = timeProvider;
        protected readonly WritableDbContext _context = context;

        public Task MapToDaoAsync(UserModel model, UserDao dao, bool skipAdminFields = false)
        {
            if (dao.UserName == dao.Email)
            {
                dao.UserName = model.Email;
            }
            dao.Email = model.Email;

            dao.FirstName = model.FirstName;
            dao.LastName = model.LastName;
            dao.PhoneNumber = model.PhoneNumber;
            dao.ModifiedAt = _timeProvider.GetUtcNow();

            if (!skipAdminFields)
            {
                var rolesToRemove = dao
                    .UserRoles.Where(ur => !model.Roles.Contains(ur.RoleId))
                    .ToList();
                var roleIdsToAdd = model
                    .Roles.Where(r => !dao.UserRoles.Any(ur => ur.RoleId == r))
                    .ToList();
                foreach (var role in rolesToRemove)
                {
                    dao.UserRoles.Remove(role);
                }
                foreach (var roleId in roleIdsToAdd)
                {
                    dao.UserRoles.Add(new UserRoleDao() { UserId = dao.Id, RoleId = roleId });
                }
                if (!model.IsActive && dao.DisabledDate is null)
                {
                    dao.DisabledDate = _timeProvider.GetUtcNow();
                }
                else
                {
                    dao.DisabledDate = null;
                }
                if (dao.CreatedAt == default)
                {
                    dao.CreatedAt = dao.ModifiedAt;
                }
            }

            return Task.CompletedTask;
        }
    }
}
