using Application.Common.Interfaces.Repositories;
using Application.Features.Base.CreatePost;

namespace Application.Features.Posts.Commands.CreatePost;

public sealed class CreatePostCommandHandler : BaseCreatePostCommandHandler<CreatePostCommand>
{
    public CreatePostCommandHandler(
        ITagRepository tagRepository, 
        IPostRepository postRepository,
        IAddressRepository addressRepository) : base(tagRepository, postRepository, addressRepository) {}
}