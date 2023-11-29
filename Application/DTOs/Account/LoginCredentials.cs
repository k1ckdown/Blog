namespace Application.DTOs.Account;

public sealed class LoginCredentials
{
    public required string Email { get; set; }
    public required string Password { get; set;  }
}