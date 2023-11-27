using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Post.Commands;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly ITagRepository _tagRepository;
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(ITagRepository tagRepository, IPostRepository postRepository)
    {
        _tagRepository = tagRepository;
        _postRepository = postRepository;
    }
    
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var tags = new List<Domain.Entities.Tag>();
        
        foreach (var id in request.CreatePostDto.Tags)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag != null) tags.Add(tag);
        }
        
        var post = new Domain.Entities.Post
        {
            Title = request.CreatePostDto.Title,
            Description = request.CreatePostDto.Description,
            ReadingTime = request.CreatePostDto.ReadingTime,
            Image = request.CreatePostDto.Image,
            CreateTime = DateTime.UtcNow,
            AddressId = request.CreatePostDto.AddressId,
            Tags = tags,
            UserId = request.UserId
        };

        await _postRepository.AddAsync(post);
        return post.Id;
    }
}