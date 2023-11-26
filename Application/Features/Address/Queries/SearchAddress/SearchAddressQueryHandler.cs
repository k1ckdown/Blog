using Application.Common.Interfaces.Repositories;
using Application.DTOs.Address;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Address.Queries.SearchAddress;

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

        var addresses = await hierarchies.Join(_addressRepository.AddressElements,
                hierarchy => hierarchy.ObjectId,
                address => address.ObjectId,
                (hierarchy, address) => _mapper.Map<SearchAddressModel>(address))
            .ToListAsync(cancellationToken: cancellationToken);

        var houses = await hierarchies.Join(_addressRepository.Houses,
                hierarchy => hierarchy.ObjectId,
                house => house.ObjectId,
                (hierarchy, house) => _mapper.Map<SearchAddressModel>(house))
            .ToListAsync(cancellationToken: cancellationToken);

        return addresses.Concat(houses)
            .Where(address => address.Text != null && address.Text.ToLower().Contains(request.Query?.ToLower() ?? ""));
    }
}