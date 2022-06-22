using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> ListAsync();
    Task<IEnumerable<Contact>> ListByUserIdAsync(int userId);
    Task<ContactResponse> SaveAsync(Contact contact);
    Task<ContactResponse> UpdateAsync(int contactId, Contact contact);
    Task<ContactResponse> DeleteAsync(int contactId);
}