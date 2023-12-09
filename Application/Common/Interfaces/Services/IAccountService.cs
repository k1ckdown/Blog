using Application.DTOs.Account;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface IAccountService
{
    Task LogOut();
    Task<TokenResponse> LogIn(LoginCredentials credentials);
    Task<TokenResponse> Register(User user, string password);
}