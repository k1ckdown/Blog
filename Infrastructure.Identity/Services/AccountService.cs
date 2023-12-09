using System.Security.Authentication;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Services;
using Application.DTOs.Account;
using Application.Wrappers;
using Infrastructure.Identity.Authentication;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services;

internal sealed class AccountService : IAccountService
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

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<TokenResponse> LogIn(LoginCredentials credentials)
    {
        var identityUser = await _userManager.FindByEmailAsync(credentials.Email);

        if (identityUser == null)
        {
            throw new InvalidCredentialException($"No accounts registered with {credentials.Email}.");
        }

        var result = await _signInManager.PasswordSignInAsync(identityUser, credentials.Password, false, false);

        if (result.Succeeded)
        {
            return new TokenResponse(_jwtProvider.Generate(identityUser));
        }

        throw new InvalidCredentialException($"Invalid credentials for {credentials.Email}.");
    }

    public async Task UpdateUser(Guid id, string email)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), id);
        }

        user.Email = email;
        await _userManager.UpdateAsync(user);
    }

    public async Task<(TokenResponse, Guid)> Register(UserRegisterModel registerModel)
    {
        var userExists = await _userManager.FindByEmailAsync(registerModel.Email);

        if (userExists != null)
        {
            throw new BadRequestException($"User with {registerModel.Email} already exists");
        }
        
        var identityUser = new ApplicationUser
        {
            Email = registerModel.Email,
            UserName = registerModel.Email
        };

        var result = await _userManager.CreateAsync(identityUser, registerModel.Password);

        if (result.Succeeded)
        {
            return (new TokenResponse(_jwtProvider.Generate(identityUser)), identityUser.Id);
        }

        throw new AuthenticationException("Register failed");
    }
}