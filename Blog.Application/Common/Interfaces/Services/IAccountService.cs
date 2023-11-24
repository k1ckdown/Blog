using Blog.Application.DTOs.Account;

namespace Blog.Application.Common.Interfaces.Services;

public interface IAccountService
{
    Task LogOut();
    Task<TokenResponse> LogIn(LoginCredentials credentials);
    Task<(TokenResponse, Guid)> Register(UserRegisterModel registerModel);
}