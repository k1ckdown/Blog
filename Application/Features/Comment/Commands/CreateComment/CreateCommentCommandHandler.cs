using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Services.Community;
using MediatR;

namespace Application.Features.Comment.Commands.CreateComment;

public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityService _communityService;

    public CreateCommentCommandHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository, 
        ICommunityService communityService)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _communityService = communityService;
    }

    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdIncludingComments(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        await _communityService.CheckAccessToPost(request.UserId, post);

        Domain.Entities.Comment? parentComment = null;
        var parentId = request.CreateCommentDto.ParentId;

        if (parentId != null)
        {
            parentComment = await _commentRepository.GetByIdAsync(parentId.Value);

            if (parentComment == null)
                throw new NotFoundException(nameof(Domain.Entities.Comment), parentId);

            if (post.Comments.All(comment => comment.Id != parentId))
                throw new BadRequestException(
                    $"Incorrect combination between post ({request.PostId}) and parent comment ({parentId})");
        }

        var comment = new Domain.Entities.Comment
        {
            ParentId = request.CreateCommentDto.ParentId,
            Content = request.CreateCommentDto.Content,
            CreateTime = DateTime.UtcNow,
            UserId = request.UserId,
            PostId = request.PostId
        };

        parentComment?.SubComments.Add(comment);
        await _commentRepository.AddAsync(comment);
    }
}