using Application.DTOs.Address;
using Application.Features.Address.Queries.GetAddressChain;
using Application.Features.Address.Queries.SearchAddress;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public sealed class AddressController : BaseController
{
    public AddressController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    [Route("chain")]
    public async Task<ActionResult<IEnumerable<SearchAddressModel>>> GetChain(Guid objectGuid)
    {
        var getAddressChainQuery = new GetAddressChainQuery(objectGuid);
        var chain = await Mediator.Send(getAddressChainQuery);
        return Ok(chain);
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<IEnumerable<SearchAddressModel>>> SearchAddress(long parentObjectId, string? query)
    {
        var searchAddressQuery = new SearchAddressQuery(parentObjectId, query);
        var addresses = await Mediator.Send(searchAddressQuery);
        return Ok(addresses);
    }
}