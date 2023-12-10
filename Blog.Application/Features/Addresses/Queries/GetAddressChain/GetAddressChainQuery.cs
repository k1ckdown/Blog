using Blog.Application.DTOs.Addresses;
using MediatR;

namespace Blog.Application.Features.Addresses.Queries.GetAddressChain;

public sealed record GetAddressChainQuery(Guid ObjectGuid) : IRequest<IEnumerable<SearchAddressModel>>;