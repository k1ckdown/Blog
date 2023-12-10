using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Features.Base.CommunityRequest;

namespace Blog.Application.Features.Communities.Commands.RejectCommunityRequest;

public sealed class RejectCommunityRequestCommandHandler 
    : BaseCommunityRequestCommandHandler<RejectCommunityRequestCommand>
{
    public RejectCommunityRequestCommandHandler(ICommunityRepository communityRepository) : base(communityRepository)
    {
    }
}