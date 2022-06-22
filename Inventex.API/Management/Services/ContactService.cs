using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Services.Communication;
using Inventex.API.Security.Domain.Repositories;
using Inventex.API.Shared.Domain.Repositories;

namespace Inventex.API.Management.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Contact>> ListAsync()
    {
        return await _contactRepository.ListAsync();
    }

    public async Task<IEnumerable<Contact>> ListByUserIdAsync(int userId)
    {
        return await _contactRepository.FindByUserIdAsync(userId);
    }

    public async Task<ContactResponse> SaveAsync(Contact contact)
    {
        // Validate UserId

        var existingUser = await _userRepository.FindByIdAsync(contact.UserId);

        if (existingUser == null)
            return new ContactResponse("Invalid User");
        
        // Validate contact Name
        try
        {
            // Add contact
            await _contactRepository.AddAsync(contact);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new ContactResponse(contact);

        }
        catch (Exception e)
        {
            // Error Handling
            return new ContactResponse($"An error occurred while saving the contact: {e.Message}");
        }

        
    }

    public async Task<ContactResponse> UpdateAsync(int contactId, Contact contact)
    {
        var existingContact = await _contactRepository.FindByIdAsync(contactId);
        
        // Validate contact

        if (existingContact == null)
            return new ContactResponse("Contact not found.");

        // Validate CategoryId

        var existingUser = await _userRepository.FindByIdAsync(contact.UserId);

        if (existingUser== null)
            return new ContactResponse("Invalid User");
        
        // Validate Title

 
        
        // Modify Fields
        existingContact.Name = contact.Name;
        existingContact.Description = contact.Description;
        existingContact.Active = contact.Active;
        existingContact.Lifetime = contact.Lifetime;

        try
        {
            _contactRepository.Update(existingContact);
            await _unitOfWork.CompleteAsync();

            return new ContactResponse(existingContact);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ContactResponse($"An error occurred while updating the contact: {e.Message}");
        }

    }

    public async Task<ContactResponse> DeleteAsync(int contactId)
    {
        var existingContact = await _contactRepository.FindByIdAsync(contactId);
        
        // Validate contact

        if (existingContact == null)
            return new ContactResponse("Contact not found.");
        
        try
        {
            _contactRepository.Remove(existingContact);
            await _unitOfWork.CompleteAsync();

            return new ContactResponse(existingContact);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ContactResponse($"An error occurred while deleting the contact: {e.Message}");
        }

    }
}