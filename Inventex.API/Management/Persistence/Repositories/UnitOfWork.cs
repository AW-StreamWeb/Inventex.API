using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Persistence.Contexts;

namespace Inventex.API.Management.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context){
        _context=context;
    }
    public async Task CompleteAsync(){
        await _context.SaveChangesAsync();
    }
}