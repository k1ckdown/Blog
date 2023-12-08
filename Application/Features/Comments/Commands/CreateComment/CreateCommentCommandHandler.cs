using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Services.Communities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Commands.CreateComment;

public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityAccessService _communityAccessService;

    public CreateCommentCommandHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository, 
        ICommunityAccessService communityAccessService)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdIncludingComments(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        await _communityAccessService.CheckAccessToPost(request.UserId, post);

        Comment? parentComment = null;
        var parentId = request.CreateCommentDto.ParentId;

        if (parentId != null)
        {
            parentComment = await _commentRepository.GetByIdAsync(parentId.Value);

            if (parentComment == null)
                throw new NotFoundException(nameof(Comment), parentId);

            if (post.Comments.All(comment => comment.Id != parentId))
                throw new BadRequestException(
                    $"Incorrect combination between post ({request.PostId}) and parent comment ({parentId})");
        }

        var comment = new Comment
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