using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Post.Commands;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly ITagRepository _tagRepository;
    private readonly IPostRepository _postRepository;
    private readonly IAddressRepository _addressRepository;

    public CreatePostCommandHandler(
        ITagRepository tagRepository, 
        IPostRepository postRepository,
        IAddressRepository addressRepository)
    {
        _tagRepository = tagRepository;
        _postRepository = postRepository;
        _addressRepository = addressRepository;
    }
    
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (!(_addressRepository.Houses.Any(house => house.ObjectGuid == request.CreatePostDto.AddressId) ||
              _addressRepository.AddressElements.Any(element => element.ObjectGuid == request.CreatePostDto.AddressId)))
        {
            throw new NotFoundException($"Address ({request.CreatePostDto.AddressId}) not found");
        }

        var tags = await GetTagList(request.CreatePostDto.Tags);
        
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

    private async Task<List<Domain.Entities.Tag>> GetTagList(IEnumerable<Guid> tagIdentifiers)
    {
        var tags = new List<Domain.Entities.Tag>();
        
        foreach (var id in tagIdentifiers)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) throw new NotFoundException(nameof(Tag), id); 
            tags.Add(tag);
        }

        return tags;
    }
}