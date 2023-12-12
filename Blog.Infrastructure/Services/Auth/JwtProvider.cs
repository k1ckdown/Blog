using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Application.Common.Exceptions;
using Blog.Application.Wrappers;
using Blog.Domain.Entities;
using Blog.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Infrastructure.Services.Auth;

internal sealed class JwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JwtProvider(IOptions<JwtOptions> jwtOptions, JwtSecurityTokenHandler tokenHandler)
    {
        _jwtOptions = jwtOptions.Value;
        _tokenHandler = tokenHandler;
    }

    public DateTime GetRefreshExpiration(string token)
    {
        var securityToken = _tokenHandler.ReadToken(token) as JwtSecurityToken;
        return securityToken?.ValidTo ?? DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshLifetimeInDays);
    }
    
    public string GetSessionId(string token)
    {
        var securityToken = _tokenHandler.ReadToken(token) as JwtSecurityToken;
        
        var sessionId = securityToken?.Payload[JwtRegisteredClaimNames.Jti]?.ToString();
        if (string.IsNullOrEmpty(sessionId)) throw new InvalidTokenException();
        
        return sessionId;
    }
    
    public TokenResponse GetTokenResponse(User user, string sessionId)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, sessionId)
        };
        
        var tokenResponse = GetTokenResponse(claims);
        return tokenResponse;
    }

    public TokenResponse GetRefreshTokenResponse(string refreshToken, string sessionId)
    {
        var claims = GetClaimsFromRefreshToken(refreshToken).ToList();
        
        var jti = new Claim(JwtRegisteredClaimNames.Jti, sessionId);
        var updatedClaims = claims.Select(claim => 
            claim.Type == JwtRegisteredClaimNames.Jti ? jti : claim).ToList();

        var tokenResponse = GetTokenResponse(updatedClaims);
        return tokenResponse;
    }

    private TokenResponse GetTokenResponse(IReadOnlyCollection<Claim> claims)
    {
        var (accessToken, expiresAt) = GenerateAccess(claims);
        var (newRefreshToken, refreshExpiresAt) = GenerateRefresh(claims);

        return new TokenResponse(accessToken, newRefreshToken, expiresAt, refreshExpiresAt);
    }

    private (string, DateTime) GenerateRefresh(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.RefreshKey));
        var expires = DateTime.UtcNow.AddDays(_jwtOptions.RefreshLifetimeInDays);
        
        var token = Generate(securityKey, expires, claims);
        return (token, expires);
    }
    
    private (string, DateTime) GenerateAccess(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.LifetimeInMinutes);

        var token = Generate(securityKey, expires, claims);
        return (token, expires);
    }

    private string Generate(SecurityKey securityKey, DateTime expires, IEnumerable<Claim> claims)
    {
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expires, 
            signingCredentials: signingCredentials);
        
        var serializedToken = _tokenHandler.WriteToken(token);
        return serializedToken;
    }
    
    private IEnumerable<Claim> GetClaimsFromRefreshToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.RefreshKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        
        if (securityToken is not JwtSecurityToken jwtSecurityToken 
            || jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, 
                StringComparison.InvariantCultureIgnoreCase) == false) throw new InvalidTokenException();

        return principal.Claims;
    }
}