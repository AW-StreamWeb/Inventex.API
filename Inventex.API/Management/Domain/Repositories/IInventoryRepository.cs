using Inventex.API.Management.Domain.Models;

namespace Inventex.API.Management.Domain.Repositories;

public interface IInventoryRepository
{
    Task<IEnumerable<Inventory>> ListAsync();
    Task AddAsync(Inventory inventory);
    Task<Inventory> FindByIdAsync(int id);
    void Update(Inventory inventory);
    void Remove(Inventory inventory);
}