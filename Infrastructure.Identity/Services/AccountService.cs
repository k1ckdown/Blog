using System.Security.Authentication;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTOs.Account;
using Application.Wrappers;
using Domain.Entities;
using Infrastructure.Identity.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Services;

internal sealed class AccountService : IAccountService
{
    private readonly JwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public AccountService(JwtProvider jwtProvider, IUserRepository userRepository)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }

    public async Task LogOut()
    {
        
    }
    
    public async Task<TokenResponse> LogIn(LoginCredentials credentials)
    {
        var user = await _userRepository.Entities
            .FirstOrDefaultAsync(user => user.Email == credentials.Email);

        if (user == null)
            throw new InvalidCredentialException($"No accounts registered with {credentials.Email}.");
        
        if (BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password) == false)
            throw new InvalidCredentialException($"Invalid credentials for {credentials.Email}.");

        return new TokenResponse(_jwtProvider.Generate(user));
    }

    public async Task<TokenResponse> Register(User user, string password)
    {
        if (await _userRepository.Entities.AnyAsync(userExists => userExists.Email == user.Email))
            throw new BadRequestException($"User with {user.Email} already exists");

        user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        await _userRepository.AddAsync(user);
            
        return new TokenResponse(_jwtProvider.Generate(user));
    }
}