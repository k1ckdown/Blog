using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Authentication;

internal sealed class JwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JwtProvider(IOptions<JwtOptions> jwtOptions, JwtSecurityTokenHandler tokenHandler)
    {
        _jwtOptions = jwtOptions.Value;
        _tokenHandler = tokenHandler;
    }

    public DateTime GetExpiration(string token)
    {
        var securityToken = _tokenHandler.ReadToken(token) as JwtSecurityToken;
        return securityToken?.ValidTo ?? DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeInMinutes);
    }
    
    public string GetTokenId(string accessToken)
    {
        var securityToken = _tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
        var tokenId = securityToken?.Payload[JwtRegisteredClaimNames.Jti]?.ToString();
        
        return tokenId ?? "";
    }
    
    public (string, DateTime) Generate(User user)
    {
        var claims = new Claim[]
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeInMinutes);
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expires, 
            signingCredentials: signingCredentials);

        var serializedToken = _tokenHandler.WriteToken(token);
        return (serializedToken, expires);
    }
}