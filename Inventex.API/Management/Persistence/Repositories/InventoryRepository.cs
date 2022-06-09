using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Inventex.API.Management.Persistence.Repositories;

public class InventoryRepository : BaseRepository, IInventoryRepository
{
    public InventoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Inventory>> ListAsync()
    {
        return await _context.Inventories.ToListAsync();
    }

    public async Task AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
    }

    public async Task<Inventory> FindByIdAsync(int id)
    {
        return await _context.Inventories.FindAsync(id);
    }

    public void Update(Inventory inventory)
    {
        _context.Inventories.Update(inventory);
    }

    public void Remove(Inventory inventory)
    {
        _context.Inventories.Remove(inventory);
    }
}