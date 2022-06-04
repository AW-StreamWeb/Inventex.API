using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Services;

public class MachineService : IMachineService
{
    private readonly IMachineRepository _machineRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public MachineService(IMachineRepository machineRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _machineRepository = machineRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Machine>> ListAsync()
    {
        return await _machineRepository.ListAsync();
    }

    public async Task<IEnumerable<Machine>> ListByCategoryIdAsync(int categoryId)
    {
        return await _machineRepository.FindByCategoryIdAsync(categoryId);
    }

    public async Task<MachineResponse> SaveAsync(Machine machine)
    {
        // Validate CategoryId

        var existingCategory = await _categoryRepository.FindByIdAsync(machine.CategoryId);

        if (existingCategory == null)
            return new MachineResponse("Invalid Category");
        
        // Validate Title

        var existingMachineWithName = await _machineRepository.FindByNameAsync(machine.Name);

        if (existingMachineWithName != null)
            return new MachineResponse("machine title already exists.");

        try
        {
            // Add machine
            await _machineRepository.AddAsync(machine);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new MachineResponse(machine);

        }
        catch (Exception e)
        {
            // Error Handling
            return new MachineResponse($"An error occurred while saving the machine: {e.Message}");
        }

        
    }

    public async Task<MachineResponse> UpdateAsync(int machineId, Machine machine)
    {
        var existingMachine = await _machineRepository.FindByIdAsync(machineId);
        
        // Validate machine

        if (existingMachine == null)
            return new MachineResponse("machine not found.");

        // Validate CategoryId

        var existingCategory = await _categoryRepository.FindByIdAsync(machine.CategoryId);

        if (existingCategory == null)
            return new MachineResponse("Invalid Category");
        
        // Validate Title

        var existingMachineWithName = await _machineRepository.FindByNameAsync(machine.Name);

        if (existingMachineWithName != null && existingMachineWithName.Id != existingMachine.Id)
            return new MachineResponse("machine title already exists.");
        
        // Modify Fields
        existingMachine.Name = machine.Name;
        existingMachine.Description = machine.Description;

        try
        {
            _machineRepository.Update(existingMachine);
            await _unitOfWork.CompleteAsync();

            return new MachineResponse(existingMachine);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new MachineResponse($"An error occurred while updating the machine: {e.Message}");
        }

    }

    public async Task<MachineResponse> DeleteAsync(int machineId)
    {
        var existingMachine = await _machineRepository.FindByIdAsync(machineId);
        
        // Validate machine

        if (existingMachine == null)
            return new MachineResponse("machine not found.");
        
        try
        {
            _machineRepository.Remove(existingMachine);
            await _unitOfWork.CompleteAsync();

            return new MachineResponse(existingMachine);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new MachineResponse($"An error occurred while deleting the machine: {e.Message}");
        }

    }
}