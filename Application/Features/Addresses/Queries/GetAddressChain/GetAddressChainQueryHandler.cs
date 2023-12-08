using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Addresses;
using AutoMapper;
using MediatR;

namespace Application.Features.Addresses.Queries.GetAddressChain;

public sealed class GetAddressChainQueryHandler : IRequestHandler<GetAddressChainQuery, IEnumerable<SearchAddressModel>>
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;

    public GetAddressChainQueryHandler(IMapper mapper, IAddressRepository addressRepository)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public async Task<IEnumerable<SearchAddressModel>> Handle(
        GetAddressChainQuery request,
        CancellationToken cancellationToken)
    {
        long objectId;
        SearchAddressModel lastAddress;
        
        var result = new List<SearchAddressModel>();
        var notFoundPath = new NotFoundException($"Address path for object ({request.ObjectGuid}) not found");

        var house = await _addressRepository.GetHouseAsync(request.ObjectGuid);
        if (house != null)
        {
            objectId = house.ObjectId;
            lastAddress = _mapper.Map<SearchAddressModel>(house);
        }
        else
        {
            var addressElement = await _addressRepository.GetAddressElementAsync(request.ObjectGuid);
            if (addressElement == null) throw notFoundPath;

            objectId = addressElement.ObjectId;
            lastAddress = _mapper.Map<SearchAddressModel>(addressElement);
        }

        var path = await _addressRepository.GetPathAsync(objectId);
        if (path == null) throw notFoundPath;

        var pathItems = path.Split(".").ToList();
        pathItems.RemoveAt(pathItems.Count - 1);

        var objectIdentifiers = pathItems.Select(item => Convert.ToInt64(item));
        foreach (var id in objectIdentifiers)
        {
            var addressElement = await _addressRepository.GetAddressElementAsync(id);
            if (addressElement != null) result.Add(_mapper.Map<SearchAddressModel>(addressElement));
        }
        result.Add(lastAddress);

        return result;
    }
}