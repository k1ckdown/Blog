using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Comment.Commands.EditComment;

public sealed class EditCommentCommandHandler : IRequestHandler<EditCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public EditCommentCommandHandler(ICommentRepository commentRepository) =>
        _commentRepository = commentRepository;
    
    public async Task Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);

        if (comment == null)
            throw new NotFoundException(nameof(Domain.Entities.Comment), request.CommentId);

        if (comment.UserId != request.UserId)
            throw new ForbiddenException($"The user ({request.UserId}) is not the author of the comment ({request.CommentId})");

        comment.Content = request.UpdateCommentDto.Content;
        comment.ModifiedDate = DateTime.UtcNow;
        
        await _commentRepository.UpdateAsync(comment);
    }
}