using Application.DTOs.Addresses;
using MediatR;

namespace Application.Features.Addresses.Queries.SearchAddress;

public sealed record SearchAddressQuery(long ParentObjectId, string? Query)
    : IRequest<IEnumerable<SearchAddressModel>>;