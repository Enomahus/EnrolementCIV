using Application.Common.Enums;
using Infrastructure.Persistence.Entities;
using Tools.Constants;

namespace Application.Features.Users.Common
{
    public class UserModel
    {
        public PersonTitle Civility { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? ServiceNumber { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public bool? IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<Guid> Roles { get; set; } = [];

        public static UserModel FromDao(UserDao user, DateTimeOffset now)
        {
            var model = new UserModel();
            MapDaoToModel(user, model, now);
            return model;
        }

        public static void MapDaoToModel(UserDao dao, UserModel model, DateTimeOffset now)
        {
            var isAdmin =
                dao.UserRoles?.Any(ur => ur.Role.Name == AppConstants.SuperAdminRole) ?? false;
            model.Civility = dao.Civility;
            model.FirstName = dao.FirstName;
            model.LastName = dao.LastName;
            model.ServiceNumber = dao.ServiceNumber;
            model.FullName = $"{dao.FirstName} {dao.LastName}".Trim();
            model.Email = dao.Email;
            model.PhoneNumber = dao.PhoneNumber;
            model.IsActive = dao.DisabledDate is null || dao.DisabledDate.Value > now;
            model.IsAdmin = isAdmin;
            model.CreatedAt = dao.CreatedAt;
            model.Roles = [.. dao.UserRoles!.Select(ur => ur.RoleId)];
        }
    }
}
