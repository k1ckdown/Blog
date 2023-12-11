using Blog.Application.Features.Base.ConsiderCommunityRequest;

namespace Blog.Application.Features.Communities.Commands.ApproveCommunityRequest;

public record ApproveCommunityRequestCommand(Guid UserId, Guid CommunityId, Guid ApplicantId) 
    : BaseConsiderCommunityRequestCommand(UserId, CommunityId, ApplicantId);