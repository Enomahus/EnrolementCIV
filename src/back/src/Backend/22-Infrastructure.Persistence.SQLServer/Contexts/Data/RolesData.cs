using Application.Common.Enums;
using Tools.Constants;

namespace Infrastructure.Persistence.SQLServer.Contexts.Data
{
    public static class RolesData
    {
        public static readonly Dictionary<AppAction, List<AppPermission>> ActionsSeed = new()
        {
            { AppAction.SuperAdmin, Enum.GetValues<AppPermission>().ToList() },
            {
                AppAction.UsersAdministration,
                [
                    AppPermission.GetUsers,
                    AppPermission.GetUser,
                    AppPermission.CreateUser,
                    AppPermission.UpdateUser,
                    AppPermission.DeleteUser,
                    AppPermission.GetAllRoles,
                ]
            },
            {
                AppAction.RegistrationRequestCreation,
                [
                    AppPermission.RegistrationsCreate,
                    AppPermission.RegistrationsUpdate,
                    AppPermission.RegistrationsDelete,
                ]
            },
            { AppAction.RegistrationRequestConsultation, [AppPermission.RegistrationsView] },
            {
                AppAction.RegistrationRequestAdministration,
                [
                    AppPermission.RegistrationsUpdate,
                    AppPermission.RegistrationsDelete,
                    AppPermission.RegistrationsUpdateStatus,
                    AppPermission.RegistrationsValidate,
                    AppPermission.RegistrationsExport,
                    AppPermission.RegistrationsView,
                ]
            },
            {
                AppAction.RegistrationRequestManagement,
                [
                    AppPermission.RegistrationsUpdate,
                    AppPermission.RegistrationRequestUpdateDraft,
                    AppPermission.GetRegistrationRequest,
                    AppPermission.GetRegistrationRequests,
                    AppPermission.GetRegistrationRequestForDetail,
                    AppPermission.RegistrationsValidate,
                ]
            },
        };

        public static readonly Dictionary<string, List<AppAction>> RolesSeed = new()
        {
            { AppConstants.SuperAdminRole, [AppAction.SuperAdmin] },
            {
                AppConstants.CommissionChairmanRole,
                [
                    AppAction.RegistrationRequestAdministration,
                    AppAction.RegistrationRequestConsultation,
                ]
            },
            {
                AppConstants.SupervisorRole,
                [
                    AppAction.RegistrationRequestConsultation,
                    AppAction.RegistrationRequestAdministration,
                ]
            },
            {
                AppConstants.DataEntryOperatorRole,
                [
                    AppAction.RegistrationRequestCreation,
                    AppAction.RegistrationRequestAdministration,
                    AppAction.RegistrationRequestConsultation,
                ]
            },
            {
                AppConstants.ElectorRole,
                [AppAction.RegistrationRequestCreation, AppAction.RegistrationRequestConsultation]
            },
        };
    }
}
