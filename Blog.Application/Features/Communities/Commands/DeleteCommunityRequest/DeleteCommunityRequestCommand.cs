using MediatR;

namespace Blog.Application.Features.Communities.Commands.DeleteCommunityRequest;

public sealed record DeleteCommunityRequestCommand(Guid UserId, Guid CommunityId) : IRequest;