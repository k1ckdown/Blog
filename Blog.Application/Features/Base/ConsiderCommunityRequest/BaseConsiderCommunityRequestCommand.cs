using MediatR;

namespace Blog.Application.Features.Base.ConsiderCommunityRequest;

public record BaseConsiderCommunityRequestCommand(Guid UserId, Guid CommunityId, Guid ApplicantId) : IRequest;