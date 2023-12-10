using System.Security.Authentication;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Application.DTOs.Account;
using Blog.Application.Wrappers;
using Blog.Domain.Entities;
using Blog.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Blog.Infrastructure.Services;

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
        var tokenId = _jwtProvider.GetSessionId(token);
        return await _cache.GetStringAsync(BlacklistedKey(tokenId)) != null;
    }

    public async Task LogOut(string token)
    {
        var sessionId = _jwtProvider.GetSessionId(token);
        var expiration = _jwtProvider.GetRefreshExpiration(token);

        await _cache.RemoveAsync(sessionId);
        await _cache.SetStringAsync(BlacklistedKey(sessionId), "revoked", new DistributedCacheEntryOptions
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

    public async Task<TokenResponse> Refresh(string refreshToken)
    {
        var tokenId = _jwtProvider.GetSessionId(refreshToken);
        var cachedRefreshToken = await _cache.GetStringAsync(tokenId);

        if (string.IsNullOrEmpty(cachedRefreshToken) || refreshToken != cachedRefreshToken)
            throw new InvalidTokenException();
        
        await _cache.RemoveAsync(tokenId);
        return await GetRefreshTokenResponse(refreshToken);
    }
    
    private async Task<TokenResponse> GetTokenResponse(User user)
    {
        var sessionId = Guid.NewGuid().ToString();
        var tokenResponse = _jwtProvider.GetTokenResponse(user, sessionId);
        
        await CacheRefreshToken(sessionId, tokenResponse.RefreshToken, tokenResponse.RefreshExpiresAt);
        return tokenResponse;
    }
    
    private async Task<TokenResponse> GetRefreshTokenResponse(string refreshToken)
    {
        var sessionId = Guid.NewGuid().ToString();
        var tokenResponse = _jwtProvider.GetRefreshTokenResponse(refreshToken, sessionId);
        
        await CacheRefreshToken(sessionId, tokenResponse.RefreshToken, tokenResponse.RefreshExpiresAt);
        return tokenResponse;
    }

    private async Task CacheRefreshToken(string sessionId, string token, DateTime expiration)
    {
        await _cache.SetStringAsync(sessionId, token, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = expiration
        });
    }
    
    private static string BlacklistedKey(string token) => $"blacklisted {token}";
}