using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Features.Base.CreatePost;

namespace Blog.Application.Features.Posts.Commands.CreatePost;

public sealed class CreatePostCommandHandler : BaseCreatePostCommandHandler<CreatePostCommand>
{
    public CreatePostCommandHandler(
        ITagRepository tagRepository, 
        IPostRepository postRepository,
        IAddressRepository addressRepository) : base(tagRepository, postRepository, addressRepository) {}
}