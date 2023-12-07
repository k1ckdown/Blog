using Application.DTOs.Community;
using Application.DTOs.Post;
using Application.Features.Community.Commands.CreateCommunityPost;
using Application.Features.Community.Commands.SubscribeToCommunity;
using Application.Features.Community.Commands.UnsubscribeFromCommunity;
using Application.Features.Community.Queries.GetCommunity;
using Application.Features.Community.Queries.GetCommunityList;
using Application.Features.Community.Queries.GetGreatestUserRoleInCommunity;
using Application.Features.Community.Queries.GetUserCommunities;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public sealed class CommunityController : BaseController
{
    public CommunityController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommunityDto>>> GetCommunityList()
    {
        var getCommunityListQuery = new GetCommunityListQuery();
        var list = await Mediator.Send(getCommunityListQuery);
        return Ok(list);
    }

    [HttpGet]
    [Route("my")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<CommunityUserDto>>> GetUserCommunities()
    {
        var getUserCommunitiesQuery = new GetUserCommunitiesQuery(UserId);
        var communities = await Mediator.Send(getUserCommunitiesQuery);
        return Ok(communities);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommunityFullDto>> GetCommunity(Guid id)
    {
        var getCommunityQuery = new GetCommunityQuery(id);
        var communityFullDto = await Mediator.Send(getCommunityQuery);
        return communityFullDto;
    }

    [HttpPost]
    [Route("{id:guid}/post")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Guid>> CreateCommunityPost(Guid id, CreatePostDto createPostDto)
    {
        var createCommunityPostCommand = new CreateCommunityPostCommand(UserId, createPostDto, id);
        var postId = await Mediator.Send(createCommunityPostCommand);
        return Ok(postId);
    }

    [HttpGet]
    [Route("{id:guid}/role")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<CommunityRole?>> GetGreatestUserRole(Guid id)
    {
        var getGreatestUserRoleInCommunityQuery = new GetGreatestUserRoleInCommunityQuery(UserId, id);
        var role = await Mediator.Send(getGreatestUserRoleInCommunityQuery);
        return Ok(role);
    }

    [HttpPost]
    [Route("{id:guid}/subscribe")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> SubscribeToCommunity(Guid id)
    {
        var subscribeToCommunityCommand = new SubscribeToCommunityCommand(UserId, id);
        await Mediator.Send(subscribeToCommunityCommand);
        return Ok();
    }

    [HttpDelete]
    [Route("{id:guid}/unsubscribe")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UnsubscribeFromCommunity(Guid id)
    {
        var unsubscribeFromCommunityCommand = new UnsubscribeFromCommunityCommand(UserId, id);
        await Mediator.Send(unsubscribeFromCommunityCommand);
        return Ok();
    }
}