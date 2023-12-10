using System.ComponentModel.DataAnnotations;
using Blog.Application.Common.ValidationAttributes;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Account;

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
    
    [PhoneNumber]
    public required string PhoneNumber { get; set; }
}