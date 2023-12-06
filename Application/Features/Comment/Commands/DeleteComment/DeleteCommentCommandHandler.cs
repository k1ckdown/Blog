using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Comment.Commands.DeleteComment;

public sealed class DeleteCommentCommandHandler : BaseCommentRequestHandler, IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository)
        : base(postRepository) => _commentRepository = commentRepository;

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdIncludingAllAsync(request.CommentId);
        if (comment == null) throw new NotFoundException(nameof(Domain.Entities.Comment), request.CommentId);
        
        await CheckAccess(request.UserId, comment.PostId);
        if (request.UserId != comment.UserId) throw new ForbiddenException(request.UserId, request.CommentId);

        if (comment.DeleteDate != null) 
            throw new BadRequestException($"Comment ({request.CommentId}) has already been deleted");
        
        if (comment.SubComments.Count > 0)
        {
            comment.Content = "";
            comment.DeleteDate = DateTime.UtcNow;
            comment.ModifiedDate = DateTime.UtcNow;
            
            await _commentRepository.UpdateAsync(comment);
        }
        else
        {
            await _commentRepository.DeleteAsync(comment);   
        }
    }
}