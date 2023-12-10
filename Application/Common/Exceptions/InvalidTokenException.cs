using System.Security.Authentication;

namespace Application.Common.Exceptions;

public sealed class InvalidTokenException : AuthenticationException
{
    public InvalidTokenException() : base("Invalid token") {}
}