using System.Security.Authentication;
using Blog.Application.Common.Interfaces.Services;
using Blog.Application.DTOs.Account;
using Blog.Domain.Entities;
using Blog.Infrastructure.Identity.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity.Services;

internal sealed class AccountService : IAccountService
{
    private readonly JwtProvider _jwtProvider;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountService(
        JwtProvider jwtProvider,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _jwtProvider = jwtProvider;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<TokenResponse> LogIn(LoginCredentials credentials)
    {
        var identityUser = await _userManager.FindByEmailAsync(credentials.Email);

        if (identityUser == null)
        {
            throw new AuthenticationException();
        }

        var result = await _signInManager.PasswordSignInAsync(identityUser, credentials.Password, false, false);

        if (result.Succeeded)
        {
            return new TokenResponse { Token = _jwtProvider.Generate(identityUser) };
        }

        throw new AuthenticationException();
    }

    public async Task<TokenResponse> Register(User user, string password)
    {
        var userExists = await _userManager.FindByEmailAsync(user.Email);

        if (userExists != null)
        {
            throw new AuthenticationException();
        }
        
        var identityUser = new IdentityUser
        {
            Id = user.Id.ToString(),
            UserName = user.Email,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(identityUser, password);

        if (result.Succeeded)
        {
            return new TokenResponse { Token = _jwtProvider.Generate(identityUser) };
        }

        throw new AuthenticationException();
    }
}