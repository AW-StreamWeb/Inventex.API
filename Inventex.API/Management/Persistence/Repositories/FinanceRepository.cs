
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Resources;
using Inventex.API.Shared.Persistence.Contexts;
using Inventex.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inventex.API.Management.Persistence.Repositories;

public class FinanceRepository: BaseRepository, IFinanceRepository
{
    public FinanceRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<Finance>> ListAsync()
    {
        return await _context.Finances
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddAsync(Finance finance)
    {
        await _context.Finances.AddAsync(finance);
    }

    public async Task<Finance> FindByIdAsync(int financeId)
    {
        return await _context.Finances
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == financeId);
    }

    public async Task<Finance> FindByNameAsync(string name)
    {
        return await _context.Finances
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Finance>> FindByUserIdAsync(int userId)
    {
        return await _context.Finances
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Finance finance)
    {
        _context.Finances.Update(finance);
    }

    public void Remove(Finance finance)
    {
        _context.Finances.Remove(finance);
    }
    
    

}