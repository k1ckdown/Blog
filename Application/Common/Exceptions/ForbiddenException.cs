namespace Application.Common.Exceptions;

public sealed class ForbiddenException : Exception
{
    public ForbiddenException() {}

    public ForbiddenException(string message) : base(message) {}
    
    public ForbiddenException(Guid userId, Guid commentId) 
        : base($"The user ({userId}) is not the author of the comment ({commentId})") {}
}