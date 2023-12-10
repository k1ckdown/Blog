using Blog.Application.Common.Exceptions.Base;

namespace Blog.Application.Common.Exceptions;

public sealed class PublicCommunityException : BadRequestException
{
    public PublicCommunityException() {}

    public PublicCommunityException(string message) : base(message) {}
    
    public PublicCommunityException(Guid communityId) : base($"The community ({communityId}) is public") {}
}