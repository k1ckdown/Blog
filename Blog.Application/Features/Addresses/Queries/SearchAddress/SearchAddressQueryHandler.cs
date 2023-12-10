using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.DTOs.Addresses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Addresses.Queries.SearchAddress;

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
            .SelectMany(hierarchy =>
                _addressRepository.AddressElements.Where(element => element.ObjectId == hierarchy.ObjectId))
            .ProjectTo<SearchAddressModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var houses = await hierarchies
            .SelectMany(hierarchy =>
                _addressRepository.AddressElements.Where(house => house.ObjectId == hierarchy.ObjectId))
            .ProjectTo<SearchAddressModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var searchAddressed = addressElements.Concat(houses);
        if (request.Query != null)
            searchAddressed = searchAddressed.Where(address =>
                address.Text != null && address.Text.ToLower().Contains(request.Query.ToLower()));
        
        return searchAddressed;
    }
}