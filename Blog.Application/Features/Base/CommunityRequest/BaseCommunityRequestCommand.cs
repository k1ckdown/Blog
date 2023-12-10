using MediatR;

namespace Blog.Application.Features.Base.CommunityRequest;

public record BaseCommunityRequestCommand(Guid UserId, Guid CommunityId, Guid ApplicantId) : IRequest;