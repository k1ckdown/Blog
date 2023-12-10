using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities.Address;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

public sealed class AddressRepository : IAddressRepository
{
    private readonly AddressesDbContext _dbContext;

    public AddressRepository(AddressesDbContext dbContext) =>
        _dbContext = dbContext;

    public IQueryable<House> Houses => _dbContext.Houses;
    public IQueryable<AddressElement> AddressElements => _dbContext.AddressElements;
    public IQueryable<AddressHierarchy> Hierarchies => _dbContext.AddressHierarchies;

    public async Task<House?> GetHouseAsync(long objectId) =>
        await Houses
            .FirstOrDefaultAsync(house => house.ObjectId == objectId);

    public async Task<House?> GetHouseAsync(Guid objectGuid) =>
        await Houses
            .FirstOrDefaultAsync(house => house.ObjectGuid == objectGuid);

    public async Task<AddressElement?> GetAddressElementAsync(long objectId) =>
        await AddressElements
            .FirstOrDefaultAsync(address => address.ObjectId == objectId);

    public async Task<AddressElement?> GetAddressElementAsync(Guid objectGuid) =>
        await AddressElements
            .FirstOrDefaultAsync(address => address.ObjectGuid == objectGuid);

    public async Task<string?> GetPathAsync(long objectId)
    {
        var hierarchy = await _dbContext.AddressHierarchies
            .FirstOrDefaultAsync(hierarchy => hierarchy.ObjectId == objectId);
        return hierarchy?.Path;
    }
}