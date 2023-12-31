using Blog.Application.DTOs.Posts;
using Blog.Application.Features.Tags.Queries.GetTagList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

public sealed class TagController : BaseController
{
    public TagController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
    {
        var getTagListQueryHandler = new GetTagListQuery();
        var tagList = await Mediator.Send(getTagListQueryHandler);
        return Ok(tagList);
    }
}