using Application.DTOs.Community;
using Application.Features.Community.Commands.SubscribeToCommunity;
using Application.Features.Community.Commands.UnsubscribeFromCommunity;
using Application.Features.Community.Queries.GetCommunity;
using Application.Features.Community.Queries.GetCommunityList;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommunityFullDto>> GetCommunity(Guid id)
    {
        var getCommunityQuery = new GetCommunityQuery(id);
        var communityFullDto = await Mediator.Send(getCommunityQuery);
        return communityFullDto;
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