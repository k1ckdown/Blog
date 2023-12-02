using Application.DTOs.Comment;
using Application.Features.Comment.Commands.CreateComment;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/")]
public sealed class CommentController : BaseController
{
    public CommentController(IMediator mediator) : base(mediator) {}

    [HttpPost]
    [Route("post/{id:guid}/comment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateComment(Guid id, CreateCommentDto createCommentDto)
    {
        var createCommentCommand = new CreateCommentCommand(UserId, id, createCommentDto);
        await Mediator.Send(createCommentCommand);
        return Ok();
    }
}