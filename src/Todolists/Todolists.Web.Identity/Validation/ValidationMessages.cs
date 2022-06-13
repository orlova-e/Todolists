namespace Todolists.Web.Identity.Validation
{
    internal static class ValidationMessages
    {
        public const string EmailRequired = "Email is required";
        public const string AccountDoesNotExist = "The account is missing in the system";
        
        public const string PasswordRequired = "The password is required";
        public const string PasswordMinLength = "The password must contain at least 8 characters";
        public const string PasswordInvalid = "The password is invalid";
    }
}
