using Application.Common.Interfaces.Repositories;
using Application.DTOs.Addresses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Addresses.Queries.SearchAddress;

public sealed class SearchAddressQueryHandler : IRequestHandler<SearchAddressQuery, IEnumerable<SearchAddressModel>>
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;

    public SearchAddressQueryHandler(IMapper mapper, IAddressRepository addressRepository)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public async Task<IEnumerable<SearchAddressModel>> Handle(SearchAddressQuery request,
        CancellationToken cancellationToken)
    {
        var hierarchies = _addressRepository.Hierarchies
            .Where(hierarchy => hierarchy.ParentObjectId == request.ParentObjectId);

        if (request.Query == null) hierarchies = hierarchies.Take(10);

        var addressElements = await hierarchies
            .Join(_addressRepository.AddressElements,
                hierarchy => hierarchy.ObjectId,
                address => address.ObjectId,
                (hierarchy, address) => _mapper.Map<SearchAddressModel>(address))
            .ToListAsync(cancellationToken: cancellationToken);

        var houses = await hierarchies
            .Join(_addressRepository.Houses,
                hierarchy => hierarchy.ObjectId,
                house => house.ObjectId,
                (hierarchy, house) => _mapper.Map<SearchAddressModel>(house))
            .ToListAsync(cancellationToken: cancellationToken);

        var result = addressElements.Concat(houses);

        if (request.Query != null)
            result = result.Where(address =>
                address.Text != null && address.Text.ToLower().Contains(request.Query.ToLower()));

        return result;
    }
}