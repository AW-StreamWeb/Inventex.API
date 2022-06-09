using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InventoryService(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
    {
        _inventoryRepository = inventoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async  Task<IEnumerable<Inventory>> ListAsync()
    {
        return await _inventoryRepository.ListAsync();
    }
    
    //POST
    public async Task<InventoryResponse> SaveAsync(Inventory inventory)
    {
        try
        {
            await _inventoryRepository.AddAsync(inventory);
            await _unitOfWork.CompleteAsync();
            return new InventoryResponse(inventory);
        }
        catch (Exception e)
        {
            return new InventoryResponse($"An error occurred while saving inventory item: {e.Message}");
        }
    }

    //PUT
    public async Task<InventoryResponse> UpdateAsync(int id, Inventory inventory)
    {
        var existingInventory = await _inventoryRepository.FindByIdAsync(id);
        if (existingInventory == null)
            return new InventoryResponse("Inventory item not found");

        existingInventory.Name = inventory.Name;
        try
        {
            _inventoryRepository.Update(existingInventory);
            await _unitOfWork.CompleteAsync();

            return new InventoryResponse(existingInventory);
        }
        catch (Exception e)
        {
            return new InventoryResponse($"An error occurred while updating inventory item: {e.Message}");
        }
    }
    
    //DELETE
    public Task<InventoryResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}