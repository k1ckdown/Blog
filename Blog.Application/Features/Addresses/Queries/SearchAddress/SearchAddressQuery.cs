using Blog.Application.DTOs.Addresses;
using MediatR;

namespace Blog.Application.Features.Addresses.Queries.SearchAddress;

public sealed record SearchAddressQuery(long ParentObjectId, string? Query)
    : IRequest<IEnumerable<SearchAddressModel>>;