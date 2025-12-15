namespace Tools.Constants
{
    public static class AppConstants
    {
        public static readonly string SuperAdminRole = "SuperAdmin";
        public static readonly string SupervisorRole = "SupervisorRole";
        public static readonly string DataEntryOperatorRole = "DataEntryOperatorRole";
        public static readonly string ElectorRole = "ElectorRole";
        public static readonly string CommissionChairmanRole = "CommissionChairmanRole";

        //App links
        public const string ConfirmPasswordResetLink = "{0}/reset-password?token={1}&email={2}";
    }
}
