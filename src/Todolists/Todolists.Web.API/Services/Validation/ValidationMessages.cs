namespace Todolists.Web.API.Services.Validation;

internal static class ValidationMessages
{
    public const string NameRequired = "The name is required";
    public const string NameLength = "The name must be between 4 and 20 characters long";
    public const string NameInvalid = "The name is invalid";
    public const string NameShouldBeUnique = "The name must be unique";
    
    public const string EmailRequired = "The email is required";
    public const string EmailInvalid = "The email is invalid";
    public const string EmailShouldBeUnique = "The email must be unique";
    
    public const string PasswordRequired = "The password is required";
    public const string PasswordMinLength = "The password must contain at least 8 characters";
    public const string PasswordInvalid = "The password is invalid";
}