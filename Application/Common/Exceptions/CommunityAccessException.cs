using Application.Common.Exceptions.Base;
using Application.Services.Communities.CommunityAccess;

namespace Application.Common.Exceptions;

public sealed class CommunityAccessException : ForbiddenException
{
    public CommunityAccessException(Guid userId, Guid communityId) 
        : base($"Access to closed community ({communityId}) is forbidden{ForUser(userId)}")
    {
    }
    
    public CommunityAccessException(
        Guid userId, Guid objectId,
        RestrictedCommunityAccessObjectType objectType)
        : base($"Access to closed community {objectType.ToString().ToLower()} ({objectId}) is forbidden{ForUser(userId)}")
    {
    }

    private static string ForUser(Guid id) => id != Guid.Empty ? $" for user {id}" : "";
}