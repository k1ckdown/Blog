using Application.DTOs.Post;
using Application.Features.Post.Commands;
using Application.Features.Post.Queries.GetPostList;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public sealed class PostController : BaseController
{
    public PostController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    public async Task<ActionResult<PostPagedListDto>> GetPostList([FromQuery] GetPostListQuery parameters)
    {
        var pagedList = await Mediator.Send(parameters);
        return pagedList;
    }
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Guid>> CreatePost(CreatePostDto createPostDto)
    {
        var createPostCommand = new CreatePostCommand(UserId, createPostDto);
        var postId = await Mediator.Send(createPostCommand);
        return postId;
    }
}