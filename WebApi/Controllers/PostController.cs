using Application.DTOs.Posts;
using Application.Features.Posts.Commands.CreatePost;
using Application.Features.Posts.Commands.DislikePost;
using Application.Features.Posts.Commands.LikePost;
using Application.Features.Posts.Queries.GetPostList;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public sealed class PostController : BaseController
{
    public PostController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PostPagedListDto>> GetPostList([FromQuery] PostSearchParameters parameters)
    {
        var getPostListQuery = new GetPostListQuery(UserId, parameters);
        var pagedList = await Mediator.Send(getPostListQuery);
        return Ok(pagedList);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreatePost(CreatePostDto createPostDto)
    {
        var createPostCommand = new CreatePostCommand(UserId, createPostDto);
        var postId = await Mediator.Send(createPostCommand);
        return Ok(postId);
    }

    [HttpPost]
    [Route("{postId:guid}/like")]
    public async Task<IActionResult> AddLike(Guid postId)
    {
        var likePostCommand = new LikePostCommand(UserId, postId);
        await Mediator.Send(likePostCommand);
        return Ok();
    }

    [HttpDelete]
    [Route("{postId:guid}/like")]
    public async Task<IActionResult> DeleteLike(Guid postId)
    {
        var dislikePostCommand = new DislikePostCommand(UserId, postId);
        await Mediator.Send(dislikePostCommand);
        return Ok();
    }
}