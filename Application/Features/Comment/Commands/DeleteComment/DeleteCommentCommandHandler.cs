using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Services.Community;
using MediatR;

namespace Application.Features.Comment.Commands.DeleteComment;

public sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityService _communityService;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository, ICommunityService communityService)
    {
        _commentRepository = commentRepository;
        _communityService = communityService;
    }

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdIncludingSubCommentsAsync(request.CommentId);
        if (comment == null) throw new NotFoundException(nameof(Domain.Entities.Comment), request.CommentId);

        await _communityService.CheckAccessToComment(request.UserId, comment);
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