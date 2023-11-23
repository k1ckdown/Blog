using System.Security.Authentication;
using Blog.Application.DTOs.Account;
using Blog.Infrastructure.Identity.Authentication;
using Blog.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity.Services;

internal sealed class AccountService
{
    private readonly JwtProvider _jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(
        JwtProvider jwtProvider,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _jwtProvider = jwtProvider;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<TokenResponse> Login(LoginCredentials credentials)
    {
        var user = await _userManager.FindByEmailAsync(credentials.Email);

        if (user == null)
        {
            throw new AuthenticationException();
        }

        var result = await _signInManager.PasswordSignInAsync(user, credentials.Password, false, false);

        if (result.Succeeded)
        {
            return new TokenResponse { Token = _jwtProvider.Generate(user) };
        }

        throw new AuthenticationException();
    }

    public async Task<TokenResponse> Register(UserRegisterModel registerModel)
    {
        var userExists = await _userManager.FindByEmailAsync(registerModel.Email);

        if (userExists != null)
        {
            throw new AuthenticationException();
        }

        var user = new ApplicationUser
        {
            FullName = registerModel.FullName,
            UserName = registerModel.Email,
            Email = registerModel.Email,
            BirthDate = registerModel.BirthDate,
            PhoneNumber = registerModel.PhoneNumber,
            Gender = registerModel.Gender,
            CreateTime = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, registerModel.Password);

        if (result.Succeeded)
        {
            return new TokenResponse { Token = _jwtProvider.Generate(user) };
        }

        throw new AuthenticationException();
    }
}