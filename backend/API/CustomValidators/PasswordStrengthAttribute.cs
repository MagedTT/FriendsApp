using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API.CustomValidators;

public class PasswordStrengthAttribute : ValidationAttribute
{
    private const string DEFAULT_ERROR_MESSAGE = "";
    private readonly int _minLength;

    public PasswordStrengthAttribute(int minLength)
    {
        _minLength = minLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Password is Required.");

        string password = Convert.ToString(value)!;

        if (password.Length < _minLength)
            return new ValidationResult($"Password must be at least {_minLength} characters long.");

        if (!Regex.IsMatch(password, @"[A-Z]"))
            return new ValidationResult("Password must contain at least one uppercase letter.");

        if (!Regex.IsMatch(password, @"[a-z]"))
            return new ValidationResult("Password must contain at least one lowercase letter.");

        if (!Regex.IsMatch(password, @"[0-9]"))
            return new ValidationResult("Password must contain at least one number.");

        if (!Regex.IsMatch(password, @"[\W_]"))
            return new ValidationResult("Password must contain at least one special character.");

        return ValidationResult.Success;
    }
}