using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Features.Base.CreatePost;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Community.Commands.CreateCommunityPost;

public sealed class CreateCommunityPostCommandHandler : BaseCreatePostCommandHandler<CreateCommunityPostCommand>
{
    private readonly ICommunityRepository _communityRepository;
    
    public CreateCommunityPostCommandHandler(
        ITagRepository tagRepository,
        IPostRepository postRepository,
        IAddressRepository addressRepository,
        ICommunityRepository communityRepository) : base(tagRepository, postRepository, addressRepository)
    {
        _communityRepository = communityRepository;
    }

    public override async Task<Guid> Handle(CreateCommunityPostCommand request, CancellationToken cancellationToken)
    {
        if (await _communityRepository.Entities.AllAsync(
                community => community.Id != request.CommunityId, 
                cancellationToken: cancellationToken))
            throw new NotFoundException(nameof(Domain.Entities.Community), request.CommunityId);

        if (await _communityRepository.Administrators.AnyAsync(
                admin => admin.UserId == request.UserId && admin.CommunityId == request.CommunityId, 
                cancellationToken) == false)
            throw new ForbiddenException(
                $"User ({request.UserId}) is not able to post in community ({request.CommunityId})");
        
        return await base.Handle(request, cancellationToken);
    }
}