using Application.DTOs.Address;
using MediatR;

namespace Application.Features.Address.Queries.SearchAddress;

public sealed record SearchAddressQuery(long ParentObjectId, string? Query)
    : IRequest<IEnumerable<SearchAddressModel>>;