using System.ComponentModel.DataAnnotations;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Account;

public sealed class UserRegisterModel
{
    [Required]
    [MinLength(1)]
    public string FullName { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
}