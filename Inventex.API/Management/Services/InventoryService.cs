using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private IInventoryService _inventoryServiceImplementation;

    public InventoryService(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
    {
        _inventoryRepository = inventoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async  Task<IEnumerable<Inventory>> ListAsync()
    {
        return await _inventoryRepository.ListAsync();
    }
    public async Task<IEnumerable<Inventory>> ListByUserIdAsync(int userId)
    {
        return await _inventoryRepository.FindByUserIdAsync(userId);
    }

    //POST
    public async Task<InventoryResponse> SaveAsync(Inventory inventory)
    {
        //Validate Inventory Id
        var existingInventory = _inventoryRepository.FindByIdAsync(inventory.Id);
        
        if (existingInventory == null)
        return new InventoryResponse("Invalid Item Inventory");
        
        //Validate Inventory Name
        var existingInventoryWithName = await _inventoryRepository.FindByNameAsync(inventory.Name);

        if (existingInventoryWithName != null)
            return new InventoryResponse("Inventory name already exists");
        try
        {
            await _inventoryRepository.AddAsync(inventory);
            await _unitOfWork.CompleteAsync();
            return new InventoryResponse(inventory);
        }
        catch (Exception e)
        {
            return new InventoryResponse($"An error occurred while saving the tutorial: {e.Message} ");
        }
    }

    //PUT
    public async Task<InventoryResponse> UpdateAsync(int inventoryId, Inventory inventory)
    {
        var existingInventory = await _inventoryRepository.FindByIdAsync(inventoryId);
        
        //Validation
        if (existingInventory == null)
            return new InventoryResponse("Inventory item not found");
        
        //Validate Inventory Name
        var existingInventoryWithName = await _inventoryRepository.FindByNameAsync(inventory.Name);

        if (existingInventoryWithName != null && existingInventoryWithName.Id!=existingInventory.Id)
            return new InventoryResponse("Inventory name already exists");

        existingInventory.Name = inventory.Name;
        existingInventory.Price = inventory.Price;
        existingInventory.Image = inventory.Image;
        existingInventory.Category = inventory.Category;
        existingInventory.InvetoryStatus = inventory.InvetoryStatus;

        try
        {
            _inventoryRepository.Update(existingInventory);
            await _unitOfWork.CompleteAsync();

            return new InventoryResponse(existingInventory);
        }
        catch (Exception e)
        {
            return new InventoryResponse($"An error occurred while updating the inventory item: {e.Message}");
        }
    }
    
    //DELETE
    public async  Task<InventoryResponse> DeleteAsync(int inventoryId)
    {
        var existingInventory = await _inventoryRepository.FindByIdAsync(inventoryId);
        //Validation
        if (existingInventory == null)
            return new InventoryResponse("Inventory item not found");
        try
        {
            _inventoryRepository.Remove(existingInventory);
            await _unitOfWork.CompleteAsync();

            return new InventoryResponse(existingInventory);
        }
        catch (Exception e)
        {
            return new InventoryResponse($"An error occurred while deleting  the inventory item: {e.Message}");
        }
    }
}