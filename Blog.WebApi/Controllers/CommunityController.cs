using Blog.Application.DTOs.Communities;
using Blog.Application.DTOs.Posts;
using Blog.Application.Features.Communities.Commands.AddCommunityRequest;
using Blog.Application.Features.Communities.Commands.ApproveCommunityRequest;
using Blog.Application.Features.Communities.Commands.CreateCommunityPost;
using Blog.Application.Features.Communities.Commands.RejectCommunityRequest;
using Blog.Application.Features.Communities.Commands.SubscribeToCommunity;
using Blog.Application.Features.Communities.Commands.UnsubscribeFromCommunity;
using Blog.Application.Features.Communities.Queries.GetCommunity;
using Blog.Application.Features.Communities.Queries.GetCommunityList;
using Blog.Application.Features.Communities.Queries.GetCommunityPostList;
using Blog.Application.Features.Communities.Queries.GetCommunityRequests;
using Blog.Application.Features.Communities.Queries.GetGreatestUserRoleInCommunity;
using Blog.Application.Features.Communities.Queries.GetUserCommunities;
using Blog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

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
        return Ok(communityFullDto);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("{id:guid}/post")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<PostPagedListDto>> GetCommunityPostList(Guid id, [FromQuery] CommunityPostSearchParameters parameters)
    {
        var getCommunityPostListQuery = new GetCommunityPostListQuery(UserId, id, parameters);
        var postId = await Mediator.Send(getCommunityPostListQuery);
        return Ok(postId);
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

    [HttpGet]
    [Route("{id:guid}/request")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<CommunityRequestDto>>> GetRequestList(Guid id)
    {
        var getCommunityRequestsQuery = new GetCommunityRequestsQuery(UserId, id);
        var requests = await Mediator.Send(getCommunityRequestsQuery);
        return Ok(requests);
    }

    [HttpPost]
    [Route("{id:guid}/request")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddRequest(Guid id)
    {
        var addCommunityRequestCommand = new AddCommunityRequestCommand(UserId, id);
        await Mediator.Send(addCommunityRequestCommand);
        return Ok();
    }

    [HttpPost]
    [Route("{communityId:guid}/approve/{applicantId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ApproveRequest(Guid communityId, Guid applicantId)
    {
        var approveCommunityRequestCommand = new ApproveCommunityRequestCommand(UserId, communityId, applicantId);
        await Mediator.Send(approveCommunityRequestCommand);
        return Ok();
    }
    
    [HttpPost]
    [Route("{communityId:guid}/reject/{applicantId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RejectRequest(Guid communityId, Guid applicantId)
    {
        var rejectCommunityRequestCommand = new RejectCommunityRequestCommand(UserId, communityId, applicantId);
        await Mediator.Send(rejectCommunityRequestCommand);
        return Ok();
    }
}