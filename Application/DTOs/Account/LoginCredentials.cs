using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account;

public sealed class LoginCredentials
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Password { get; set; }
}