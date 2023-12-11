using Blog.Application.Common.Exceptions.Base;

namespace Blog.Application.Common.Exceptions;

public sealed class CommunityRequestNotFoundException : NotFoundException
{
    public CommunityRequestNotFoundException() {}
    
    public CommunityRequestNotFoundException(string message) : base(message) {}

    public CommunityRequestNotFoundException(Guid userId, Guid communityId)
        : base($"The user's ({userId}) request to join the community ({communityId}) was not found")
    {
    }
}