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

    public async Task<IEnumerable<SearchAddressModel>> Handle(SearchAddressQuery request, CancellationToken cancellationToken)
    {
        var query = request.Query?.ToLower();
        var hierarchies = _addressRepository.Hierarchies
            .Where(hierarchy => hierarchy.ParentObjectId == request.ParentObjectId);

        if (query == null) hierarchies = hierarchies.Take(10);
        
        var addressElements = hierarchies
            .Join(_addressRepository.AddressElements,
                hierarchy => hierarchy.ObjectId,
                element => element.ObjectId,
                (hierarchy, element) => element);
        
        var houses = hierarchies
            .Join(_addressRepository.Houses,
                hierarchy => hierarchy.ObjectId,
                house => house.ObjectId,
                (hierarchy, house) => house);

        if (query != null)
        {
            addressElements = addressElements
                .Where(element => element.Name.ToLower().Contains(query));

            houses = houses
                .Where(house =>
                    (house.Number != null && house.Number.ToLower().Contains(query)) 
                    || (house.FirstAdditionalNumber != null && house.FirstAdditionalNumber.ToLower().Contains(query)) 
                    || (house.SecondAdditionalNumber != null && house.SecondAdditionalNumber.ToLower().Contains(query)));
        }
        
        var searchModelsFromElements = await addressElements
            .ProjectTo<SearchAddressModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        var searchModelsFromHouses = await houses
            .ProjectTo<SearchAddressModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return searchModelsFromElements.Concat(searchModelsFromHouses);
    }
}