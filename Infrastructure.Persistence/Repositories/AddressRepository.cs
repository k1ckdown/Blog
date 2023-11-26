using Application.Common.Interfaces.Repositories;
using Domain.Entities.Address;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class AddressRepository : IAddressRepository
{
    private readonly AddressesDbContext _dbContext;


    public AddressRepository(AddressesDbContext dbContext) => 
        _dbContext = dbContext;

    public async Task<House?> GetHouseAsync(Guid objectGuid)
    {
        return await _dbContext.Houses
            .FirstOrDefaultAsync(house => house.ObjectGuid == objectGuid);
    }
    
    public async Task<AddressElement?> GetAddressElementAsync(long objectId)
    {
        return await _dbContext.AddressElements
            .FirstOrDefaultAsync(address => address.ObjectId == objectId && address.IsActual == 1);
    }
    
    public async Task<AddressElement?> GetAddressElementAsync(Guid objectGuid)
    {
        return await _dbContext.AddressElements
            .FirstOrDefaultAsync(address => address.ObjectGuid == objectGuid && address.IsActual == 1);
    }
    
    public async Task<string?> GetPathAsync(long objectId)
    {
        var hierarchy = await _dbContext.AddressHierarchies
            .FirstOrDefaultAsync(hierarchy => hierarchy.ObjectId == objectId);
        return hierarchy?.Path;
    }
}