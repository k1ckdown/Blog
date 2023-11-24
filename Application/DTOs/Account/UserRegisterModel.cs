using System.ComponentModel.DataAnnotations;
using Application.Common.ValidationAttributes;
using Domain.Entities;

namespace Application.DTOs.Account;

public sealed class UserRegisterModel
{
    [Required]
    [MinLength(1)]
    public required string FullName { get; set; }
    
    [Required]
    [MinLength(6)]
    [ContainsDigit]
    public required string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [DateLessThanToday]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public required Gender Gender { get; set; }
    
    [RegularExpression(@"^(\+7|8)\d{10}$")]
    public required string PhoneNumber { get; set; }
}