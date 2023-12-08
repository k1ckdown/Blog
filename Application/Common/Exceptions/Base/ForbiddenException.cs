namespace Application.Common.Exceptions.Base;

public class ForbiddenException : Exception
{
    public ForbiddenException() {}

    public ForbiddenException(string message) : base(message) {}
}