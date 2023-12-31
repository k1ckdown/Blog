using Blog.Application.Services.Communities;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Comments.Commands.DeleteComment;

public sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityAccessService _communityAccessService;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository, ICommunityAccessService communityAccessService)
    {
        _commentRepository = commentRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdIncludingSubCommentsAsync(request.CommentId);
        if (comment == null) throw new NotFoundException(nameof(Comment), request.CommentId);

        await _communityAccessService.CheckAccessToComment(request.UserId, comment);
        if (request.UserId != comment.UserId) throw new AuthorCommentException(request.UserId, request.CommentId);

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