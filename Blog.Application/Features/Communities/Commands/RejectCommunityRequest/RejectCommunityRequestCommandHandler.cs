using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Features.Base.ConsiderCommunityRequest;

namespace Blog.Application.Features.Communities.Commands.RejectCommunityRequest;

public sealed class RejectCommunityRequestCommandHandler 
    : BaseConsiderCommunityRequestCommandHandler<RejectCommunityRequestCommand>
{
    public RejectCommunityRequestCommandHandler(ICommunityRepository communityRepository) : base(communityRepository)
    {
    }
}