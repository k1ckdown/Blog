using Application.Common.Exceptions.Base;

namespace Application.Common.Exceptions;

public sealed class AuthorCommentException : ForbiddenException
{
    public AuthorCommentException(Guid userId, Guid commentId) 
        : base($"The user ({userId}) is not the author of the comment ({commentId})") {}
}