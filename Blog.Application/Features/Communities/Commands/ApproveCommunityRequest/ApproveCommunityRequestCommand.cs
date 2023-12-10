using MediatR;

namespace Blog.Application.Features.Communities.Commands.ApproveCommunityRequest;

public sealed record ApproveCommunityRequestCommand(Guid UserId, Guid CommunityId, Guid ApplicantId)
    : IRequest;