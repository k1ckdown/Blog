using Application.DTOs.Authors;
using Application.Features.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public sealed class AuthorController : BaseController
{
    public AuthorController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorList()
    {
        var getAuthorListQuery = new GetAuthorListQuery();
        var authors = await Mediator.Send(getAuthorListQuery);
        return Ok(authors);
    }
}