using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Inventex.API.Management.Persistence.Repositories;

public class MachineRepository : BaseRepository, IMachineRepository
{
    public MachineRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Machine>> ListAsync()
    {
        return await _context.Machines
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task AddAsync(Machine machine)
    {
        await _context.Machines.AddAsync(machine);
    }

    public async Task<Machine> FindByIdAsync(int machineId)
    {
        return await _context.Machines
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == machineId);
    }

    public async Task<Machine> FindByNameAsync(string name)
    {
        return await _context.Machines
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Machine>> FindByCategoryIdAsync(int categoryId)
    {
        return await _context.Machines
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public void Update(Machine machine)
    {
        _context.Machines.Update(machine);
    }

    public void Remove(Machine machine)
    {
        _context.Machines.Remove(machine);
    }
}