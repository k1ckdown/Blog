using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Common.ValidationAttributes;

public sealed class DateLessThanTodayAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value != null && (DateTime)value < DateTime.UtcNow
            ? ValidationResult.Success
            : new ValidationResult("Birth date can't be later than today");
}

