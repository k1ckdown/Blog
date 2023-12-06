using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Comment.Commands.EditComment;

public sealed class EditCommentCommandHandler : BaseCommentRequestHandler, IRequestHandler<EditCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public EditCommentCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository) 
        : base(postRepository) => _commentRepository = commentRepository;
    
    public async Task Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null) throw new NotFoundException(nameof(Domain.Entities.Comment), request.CommentId);

        await CheckAccess(request.UserId, comment.PostId);
        if (request.UserId != comment.UserId) throw new ForbiddenException(request.UserId, request.CommentId);

        comment.Content = request.UpdateCommentDto.Content;
        comment.ModifiedDate = DateTime.UtcNow;
        
        await _commentRepository.UpdateAsync(comment);
    }
}