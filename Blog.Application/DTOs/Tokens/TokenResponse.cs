namespace Blog.Application.DTOs.Tokens;

public sealed record TokenResponse(
    string AccessToken, 
    string RefreshToken, 
    DateTime ExpiresAt,
    DateTime RefreshExpiresAt);