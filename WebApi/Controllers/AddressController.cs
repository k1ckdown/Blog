using Application.Features.Address.Queries.GetAddressChain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public sealed class AddressController : BaseController
{
    public AddressController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    [Route("chain")]
    public async Task<IActionResult> GetChain(Guid objectGuid)
    {
        var query = new GetAddressChainQuery(objectGuid);
        var chain = await Mediator.Send(query);
        return Ok(chain);
    }
}