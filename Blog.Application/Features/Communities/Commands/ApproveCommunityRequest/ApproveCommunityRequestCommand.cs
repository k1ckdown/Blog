using Blog.Application.Features.Base.CommunityRequest;

namespace Blog.Application.Features.Communities.Commands.ApproveCommunityRequest;

public record ApproveCommunityRequestCommand(Guid UserId, Guid CommunityId, Guid ApplicantId) 
    : BaseCommunityRequestCommand(UserId, CommunityId, ApplicantId);