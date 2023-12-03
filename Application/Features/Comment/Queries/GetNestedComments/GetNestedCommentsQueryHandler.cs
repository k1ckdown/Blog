using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Comment;
using AutoMapper;
using MediatR;

namespace Application.Features.Comment.Queries.GetNestedComments;

public sealed class GetNestedCommentsQueryHandler : IRequestHandler<GetNestedCommentsQuery, IEnumerable<CommentDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _commentRepository;

    public GetNestedCommentsQueryHandler(IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetNestedCommentsQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdIncludingAllAsync(request.CommentId);

        if (comment == null) throw new NotFoundException(nameof(Domain.Entities.Comment), request.CommentId);

        var subComments = _mapper.Map<List<Domain.Entities.Comment>, IEnumerable<CommentDto>>(comment.SubComments);
        return subComments;
    }
}