using Application.DTOs.Addresses;
using MediatR;

namespace Application.Features.Addresses.Queries.GetAddressChain;

public sealed record GetAddressChainQuery(Guid ObjectGuid) : IRequest<IEnumerable<SearchAddressModel>>;