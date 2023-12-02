using Application.DTOs.Community;
using Application.Features.Community.Queries.GetCommunityList;
using MediatR;
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
}