using System.Security.Authentication;
using System.Security.Cryptography;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTOs.Account;
using Application.Wrappers;
using Domain.Entities;
using Infrastructure.Identity.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Identity.Services;

internal sealed class AuthService : IAuthService
{
    private readonly IDistributedCache _cache;
    private readonly JwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public AuthService(IDistributedCache cache, JwtProvider jwtProvider, IUserRepository userRepository)
    {
        _cache = cache;
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }

    public async Task<bool> IsRevoked(string token)
    {
        var tokenId = _jwtProvider.GetTokenId(token);
        return await _cache.GetStringAsync(BlacklistedKey(tokenId)) != null;
    }

    public async Task LogOut(string token)
    {
        var tokenId = _jwtProvider.GetTokenId(token);
        var expiration = _jwtProvider.GetExpiration(token);
        
        await _cache.SetStringAsync(BlacklistedKey(tokenId), "revoked", new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = expiration
        });
    }
    
    public async Task<TokenResponse> LogIn(LoginCredentials credentials)
    {
        var user = await _userRepository.Entities
            .FirstOrDefaultAsync(user => user.Email == credentials.Email);

        if (user == null)
            throw new InvalidCredentialException($"No accounts registered with {credentials.Email}.");
        
        if (BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password) == false)
            throw new InvalidCredentialException($"Invalid credentials for {credentials.Email}.");

        return await GetTokenResponse(user);
    }

    public async Task<TokenResponse> Register(User user, string password)
    {
        if (await _userRepository.Entities.AnyAsync(userExists => userExists.Email == user.Email))
            throw new BadRequestException($"User with {user.Email} already exists");

        user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        await _userRepository.AddAsync(user);

        return await GetTokenResponse(user);
    }

    public async Task<TokenResponse> Refresh(RefreshTokenRequest request)
    {
        var userId = await _cache.GetStringAsync(request.Token);
        if (userId == null) throw new UnauthorizedAccessException("Invalid Refresh Token");
        
        var user = await _userRepository.GetByIdAsync(new Guid(userId));
        if (user == null) throw new NotFoundException(nameof(User), userId);

        await _cache.RemoveAsync(request.Token);
        return await GetTokenResponse(user);
    }

    private async Task<TokenResponse> GetTokenResponse(User user)
    {
        var (token, expiresAt)  = _jwtProvider.Generate(user);
        var refreshToken = await GenerateRefreshToken(user.Id);

        return new TokenResponse(token, refreshToken, expiresAt);
    }
    
    private async Task<string> GenerateRefreshToken(Guid userId)
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        await _cache.SetStringAsync(refreshToken, userId.ToString(), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(60)
        });

        return refreshToken;
    }

    private static string BlacklistedKey(string token) => $"blacklisted {token}";
}