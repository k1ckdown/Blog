using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTOs.Comments;
using Application.Services.Communities;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Queries.GetNestedComments;

public sealed class GetNestedCommentsQueryHandler : IRequestHandler<GetNestedCommentsQuery, IEnumerable<CommentDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityAccessService _communityAccessService;
    
    public GetNestedCommentsQueryHandler(
        IMapper mapper, 
        ICommentRepository commentRepository,
        ICommunityAccessService communityAccessService)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetNestedCommentsQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdIncludingAllAsync(request.CommentId);
        if (comment == null) throw new NotFoundException(nameof(Comment), request.CommentId);

        await _communityAccessService.CheckAccessToComment(request.UserId, comment);
        if (comment.ParentId != null)
            throw new BadRequestException($"Comment ({request.CommentId}) is not a root element");

        var nestedComments = new List<Comment>();
        var queue = new Queue<Comment>(comment.SubComments);
        
        while (queue.Count > 0)
        {
            var currentComment = await _commentRepository.GetByIdIncludingAllAsync(queue.Dequeue().Id);
            if (currentComment == null) continue;
            
            nestedComments.Add(currentComment);
            foreach (var subComment in currentComment.SubComments)
            {
                queue.Enqueue(subComment);
            }
        }

        var nestedCommentListDto = _mapper.Map<List<Comment>, IEnumerable<CommentDto>>(nestedComments);
        return nestedCommentListDto;
    }
}