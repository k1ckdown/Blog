using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Services.Community;
using MediatR;

namespace Application.Features.Comment.Commands.EditComment;

public sealed class EditCommentCommandHandler : IRequestHandler<EditCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityService _communityService;

    public EditCommentCommandHandler(ICommentRepository commentRepository, ICommunityService communityService)
    {
        _commentRepository = commentRepository;
        _communityService = communityService;
    }
    
    public async Task Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null) throw new NotFoundException(nameof(Domain.Entities.Comment), request.CommentId);

        await _communityService.CheckAccessToComment(request.UserId, comment);
        if (request.UserId != comment.UserId) throw new ForbiddenException(request.UserId, request.CommentId);

        comment.Content = request.UpdateCommentDto.Content;
        comment.ModifiedDate = DateTime.UtcNow;
        
        await _commentRepository.UpdateAsync(comment);
    }
}