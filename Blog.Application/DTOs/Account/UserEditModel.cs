using System.ComponentModel.DataAnnotations;
using Blog.Application.Common.ValidationAttributes;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Account;

public sealed class UserEditModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    [MinLength(1)]
    public required string FullName { get; set; }
    
    [DateLessThanToday]
    public DateTime? BirthDate { get; set; }
    
    [Required]
    public required Gender Gender { get; set; }
    
    [PhoneNumber]
    public string? PhoneNumber { get; set; }
}