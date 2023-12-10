using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Common.ValidationAttributes;

public sealed class ContainsDigitAttribute : ValidationAttribute
{
    public override string FormatErrorMessage(string name) =>
        $"{name} requires at least one digit";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value != null && ((string)value).Any(char.IsDigit)
            ? ValidationResult.Success
            : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
}