namespace Application.Common.Exceptions;

public sealed class ForbiddenException : Exception
{
    public ForbiddenException() {}

    public ForbiddenException(string message) : base(message) {}
}