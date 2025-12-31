using System.ComponentModel.DataAnnotations;
using API.CustomValidators;

namespace API.DTOs;

public class RegisterDTO
{
    [Required(ErrorMessage = "{0} is Required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "{0} is Required.")]
    public string Name { get; set; } = string.Empty;


    [DataType(DataType.Password)]
    [Required(ErrorMessage = "{0} is Required.")]
    [PasswordStrength(8, ErrorMessage = "Password does not meet the strength requirements.")]
    public string Password { get; set; } = string.Empty;
}