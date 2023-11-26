using Domain.Entities.Address;

namespace Application.Common.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<string?> GetPathAsync(long objectId);
    Task<House?> GetHouseAsync(Guid objectGuid);
    Task<AddressElement?> GetAddressElementAsync(long objectId);
    Task<AddressElement?> GetAddressElementAsync(Guid objectGuid);
}