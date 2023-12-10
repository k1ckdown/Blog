using Blog.Application.DTOs.Account;
using Blog.Application.Wrappers;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Services;

public interface IAuthService
{
    Task<bool> IsRevoked(string token);
    Task LogOut(string token);
    Task<TokenResponse> LogIn(LoginCredentials credentials);
    Task<TokenResponse> Register(User user, string password);
    Task<TokenResponse> Refresh(string refreshToken);
}