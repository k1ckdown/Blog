using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs.Account;

public sealed class UserRegisterModel
{
    [Required]
    [MinLength(1)]
    public required string FullName { get; set; }
    
    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    [Required]
    public required Gender Gender { get; set; }
    
    [Phone]
    public required string PhoneNumber { get; set; }
}