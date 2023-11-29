using Application.DTOs.Address;
using MediatR;

namespace Application.Features.Address.Queries.GetAddressChain;

public sealed record GetAddressChainQuery(Guid ObjectGuid) : IRequest<IEnumerable<SearchAddressModel>>;