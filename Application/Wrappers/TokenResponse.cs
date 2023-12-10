namespace Application.Wrappers;

public sealed record TokenResponse(
    string AccessToken, 
    string RefreshToken, 
    DateTime ExpiresAt,
    DateTime RefreshExpiresAt);