using MediatR;

namespace Blog.Application.Features.Communities.Commands.AddCommunityRequest;

public sealed record AddCommunityRequestCommand(Guid UserId, Guid CommunityId) : IRequest;