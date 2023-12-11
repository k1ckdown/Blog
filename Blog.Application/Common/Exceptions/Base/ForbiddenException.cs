namespace Blog.Application.Common.Exceptions.Base;

public class ForbiddenException : Exception
{
    public ForbiddenException() {}

    public ForbiddenException(string message) : base(message) {}

    public ForbiddenException(Guid userId) : base($"Access is forbidden for user ({userId})") {}
}