using Application.DTOs.Account;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface IAuthService
{
    Task<bool> IsRevoked(string token);
    Task LogOut(string token);
    Task<TokenResponse> LogIn(LoginCredentials credentials);
    Task<TokenResponse> Register(User user, string password);
    Task<TokenResponse> Refresh(RefreshTokenRequest request);
}