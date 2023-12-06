using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Comment.Commands.CreateComment;

public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository) =>
        _commentRepository = commentRepository;
    
    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Domain.Entities.Comment
        {
            ParentId = request.CreateCommentDto.ParentId, 
            Content = request.CreateCommentDto.Content,
            CreateTime = DateTime.UtcNow,
            UserId = request.UserId,
            PostId = request.PostId
        };

        var parentId = request.CreateCommentDto.ParentId;
        if (parentId != null)
        {
            var parentComment = await _commentRepository.GetByIdAsync(parentId.Value);
            if (parentComment == null) throw new NotFoundException(nameof(Domain.Entities.Comment), parentId);
            parentComment.SubComments.Add(comment);
        }

        await _commentRepository.AddAsync(comment);
    }
}