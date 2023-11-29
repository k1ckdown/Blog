using Application.DTOs.Account;
using Application.Wrappers;

namespace Application.Common.Interfaces.Services;

public interface IAccountService
{
    Task LogOut();
    Task<TokenResponse> LogIn(LoginCredentials credentials);
    Task UpdateUser(Guid id, string email);
    Task<(TokenResponse, Guid)> Register(UserRegisterModel registerModel);
}