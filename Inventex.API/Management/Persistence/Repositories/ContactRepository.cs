using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Shared.Persistence.Contexts;
using Inventex.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inventex.API.Management.Persistence.Repositories;

public class ContactRepository: BaseRepository, IContactRepository
{
    public ContactRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Contact>> ListAsync()
    {
        return await _context.Contacts
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
    }

    public async Task<Contact> FindByIdAsync(int Id)
    {
        return await _context.Contacts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == Id);
    }

    public async Task<Contact> FindByNameAsync(string name)
    {
        return await _context.Contacts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Contact>> FindByUserIdAsync(int userId)
    {
        return await _context.Contacts
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Contact contact)
    {
        _context.Contacts.Update(contact);
    }

    public void Remove(Contact contact)
    {
        _context.Contacts.Remove(contact);
    }
}