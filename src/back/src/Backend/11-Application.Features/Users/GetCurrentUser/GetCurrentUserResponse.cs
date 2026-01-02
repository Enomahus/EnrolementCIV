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
            var response = new GetCurrentUserResponse();
            UserModel.MapDaoToModel(userDao, response, now);
            response.Id = userDao.Id;
            response.Permissions = perms;
            return response;
        }
    }
}
