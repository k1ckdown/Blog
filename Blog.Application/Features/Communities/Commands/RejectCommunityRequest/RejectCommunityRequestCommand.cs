using Blog.Application.Features.Base.CommunityRequest;

namespace Blog.Application.Features.Communities.Commands.RejectCommunityRequest;

public record RejectCommunityRequestCommand(Guid UserId, Guid CommunityId, Guid ApplicantId) 
    : BaseCommunityRequestCommand(UserId, CommunityId, ApplicantId);