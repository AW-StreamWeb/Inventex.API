using Inventex.API.Management.Domain.Models;

namespace Inventex.API.Management.Domain.Repositories;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> ListAsync();
    Task AddAsync(Contact contact);
    Task<Contact> FindByIdAsync(int Id);
    Task<Contact> FindByNameAsync(string name);
    Task<IEnumerable<Contact>> FindByUserIdAsync(int userId);
    void Update(Contact contact);
    void Remove(Contact contact);
}