using Inventex.API.Management.Persistence.Contexts;

namespace Inventex.API.Management.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;
    public BaseRepository(AppDbContext context){
        _context=context;
    }
}
