using Domain.Entities.Address;

namespace Application.Common.Interfaces.Repositories;

public interface IAddressRepository
{
    IQueryable<House> Houses { get; }
    IQueryable<AddressElement> AddressElements { get; }
    IQueryable<AddressHierarchy> Hierarchies { get; }
    Task<string?> GetPathAsync(long objectId);
    Task<House?> GetHouseAsync(long objectId);
    Task<House?> GetHouseAsync(Guid objectGuid);
    Task<AddressElement?> GetAddressElementAsync(long objectId);
    Task<AddressElement?> GetAddressElementAsync(Guid objectGuid);
}