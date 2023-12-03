using Application.DTOs.Comment;
using Application.Features.Comment.Commands.CreateComment;
using Application.Features.Comment.Commands.EditComment;
using Application.Features.Comment.Queries.GetNestedComments;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/")]
public sealed class CommentController : BaseController
{
    public CommentController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    [Route("comment/{id:guid}/tree")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetNestedComments(Guid id)
    {
        var getNestedCommentsQuery = new GetNestedCommentsQuery(id);
        var comments = await Mediator.Send(getNestedCommentsQuery);
        return Ok(comments);
    }
    
    [HttpPost]
    [Route("post/{id:guid}/comment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateComment(Guid id, CreateCommentDto createCommentDto)
    {
        var createCommentCommand = new CreateCommentCommand(UserId, id, createCommentDto);
        await Mediator.Send(createCommentCommand);
        return Ok();
    }

    [HttpPut]
    [Route("comment/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> EditComment(Guid id, UpdateCommentDto updateCommentDto)
    {
        var editCommentCommand = new EditCommentCommand(UserId, id, updateCommentDto);
        await Mediator.Send(editCommentCommand);
        return Ok();
    }
}