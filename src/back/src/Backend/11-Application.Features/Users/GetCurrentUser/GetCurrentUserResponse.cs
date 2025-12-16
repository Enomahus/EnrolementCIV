using Application.Common.Enums;
using Application.Features.Users.Common;
using Infrastructure.Persistence.Entities;
using Tools.Constants;

namespace Application.Features.Users.GetCurrentUser
{
    public class GetCurrentUserResponse : UserModel
    {
        public Guid Id { get; private set; }
        public bool IsSuperAdmin { get; set; }
        public List<AppPermission> Permissions { get; set; } = [];

        public static GetCurrentUserResponse FromDao(
            UserDao userDao,
            List<AppPermission> perms,
            DateTimeOffset now
        )
        {
            //var isSuperAdmin =
            //    userDao.UserRoles?.Any(ur => ur.Role.Name == AppConstants.SuperAdminRole) ?? false;
            //var fullName = $"{userDao.FirstName} {userDao.LastName}".Trim();

            //return new GetCurrentUserResponse
            //{
            //    Id = userDao.Id,
            //    Civility = userDao.Civility,
            //    FirstName = userDao.FirstName,
            //    LastName = userDao.LastName,
            //    FullName = fullName,
            //    Email = userDao.Email ?? "",
            //    PhoneNumber = userDao.PhoneNumber,
            //    IsSuperAdmin = isSuperAdmin,
            //    Permissions = perms,
            //};

            var response = new GetCurrentUserResponse();
            UserModel.MapDaoToModel(userDao, response, now);
            response.Id = userDao.Id;
            response.Permissions = perms;
            return response;
        }
    }
}
