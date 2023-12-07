using Domain.Entities;

namespace Application.Services.Community;

public interface ICommunityService
{
    Task CheckAccessToPost(Guid userId, Post post);
    Task CheckAccessToComment(Guid userId, Comment comment);
}