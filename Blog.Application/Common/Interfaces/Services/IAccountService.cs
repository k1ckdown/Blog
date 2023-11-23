using Blog.Application.DTOs.Account;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Services;

public interface IAccountService
{
    Task LogOut();
    Task<TokenResponse> LogIn(LoginCredentials credentials);
    Task<TokenResponse> Register(User user, string password);
}