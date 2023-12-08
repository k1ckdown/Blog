using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Services.Communities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Commands.EditComment;

public sealed class EditCommentCommandHandler : IRequestHandler<EditCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityAccessService _communityAccessService;

    public EditCommentCommandHandler(ICommentRepository commentRepository, ICommunityAccessService communityAccessService)
    {
        _commentRepository = commentRepository;
        _communityAccessService = communityAccessService;
    }
    
    public async Task Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null) throw new NotFoundException(nameof(Comment), request.CommentId);

        await _communityAccessService.CheckAccessToComment(request.UserId, comment);
        if (request.UserId != comment.UserId) throw new AuthorCommentException(request.UserId, request.CommentId);

        comment.Content = request.UpdateCommentDto.Content;
        comment.ModifiedDate = DateTime.UtcNow;
        
        await _commentRepository.UpdateAsync(comment);
    }
}