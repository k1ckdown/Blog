using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Common.ValidationAttributes;

public sealed partial class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value != null && PhoneRegex().IsMatch((string)value)
            ? ValidationResult.Success
            : new ValidationResult("Phone number is not a valid");

    [GeneratedRegex(@"^(\+7|8)\d{10}$")]
    private static partial Regex PhoneRegex();
}