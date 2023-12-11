using Blog.Application.Features.Base.ConsiderCommunityRequest;

namespace Blog.Application.Features.Communities.Commands.RejectCommunityRequest;

public record RejectCommunityRequestCommand(Guid UserId, Guid CommunityId, Guid ApplicantId) 
    : BaseConsiderCommunityRequestCommand(UserId, CommunityId, ApplicantId);