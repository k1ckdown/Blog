using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface ICommunityAccessService
{
    Task CheckAccess(Guid userId, Community community);
    Task CheckAccessToPost(Guid userId, Post post);
    Task CheckAccessToComment(Guid userId, Comment comment);
}