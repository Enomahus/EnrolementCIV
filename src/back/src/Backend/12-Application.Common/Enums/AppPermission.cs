namespace Application.Common.Enums
{
    public enum AppPermission
    {
        // Dossiers d’inscription
        RegistrationsView,
        RegistrationsCreate,
        RegistrationsUpdate,
        RegistrationsDelete,
        RegistrationsValidate,
        RegistrationsExport,
        RegistrationsUpdateStatus,
        GetRegistrationRequest,
        GetRegistrationRequests,
        GetRegistrationRequestForDetail,
        RegistrationRequestUpdateDraft,

        // Administration
        SuperAdmin,
        CreateUser,
        DeleteUser,
        GetAllUsers,
        GetCurrentUser,
        GetUser,
        GetUsers,
        ToggleActiveUser,
        UpdateUser,

        // Reporting / Audit
        Reports_View,
        Audit_View,
    }
}
