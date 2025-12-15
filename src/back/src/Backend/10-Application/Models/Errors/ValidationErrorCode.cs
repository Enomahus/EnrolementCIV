namespace Application.Models.Errors
{
    public enum ValidationErrorCode
    {
        Required = 0,
        MaxLength,
        Unique,
        Base64Format,
        InvalidPassword,
        PositiveNumber,
        GreaterThanZero,
        UserMustExist,
        RoleMustExist,
        UserLinked,
        InvalidEmail,
        InvalidEnum,
    }
}
