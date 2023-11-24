using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account;

public sealed class LoginCredentials
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(1)]
    public required string Password { get; set;  }
}