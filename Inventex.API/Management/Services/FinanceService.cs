using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Services.Communication;
using Inventex.API.Security.Domain.Repositories;
using Inventex.API.Shared.Domain.Repositories;

namespace Inventex.API.Management.Services;

public class FinanceService:IFinanceService
{
    private readonly IFinanceRepository _financeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;


    public FinanceService(IFinanceRepository financeRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _financeRepository = financeRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
     public async Task<IEnumerable<Finance>> ListAsync()
     {
         return await _financeRepository.ListAsync();
     }

    public async Task<IEnumerable<Finance>> ListByUserIdAsync(int userId)
    {
        return await _financeRepository.FindByUserIdAsync(userId);
    }

    public async Task<FinanceResponse> SaveAsync(Finance finance)
    {
        // Validate CategoryId

        var existingUser = await _userRepository.FindByIdAsync(finance.UserId);

        if (existingUser == null)
            return new FinanceResponse("Invalid User");
        
        // Validate Title


        try
        {
            // Add Tutorial
            await _financeRepository.AddAsync(finance);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new FinanceResponse(finance);

        }
        catch (Exception e)
        {
            // Error Handling
            return new FinanceResponse($"An error occurred while saving the finance: {e.Message}");
        }

        
    }

    public async Task<FinanceResponse> UpdateAsync(int financeId, Finance finance)
    {
        var existingFinance = await _financeRepository.FindByIdAsync(financeId);


        if (existingFinance == null)
            return new FinanceResponse("Finance not found.");



        var existingUser = await _userRepository.FindByIdAsync(finance.UserId);

        if (existingUser == null)
            return new FinanceResponse("Invalid User");
        
        // Validate Title
        
        
        // Modify Fields
        existingFinance.Name = finance.Name;
        existingFinance.Day = finance.Day;
        existingFinance.Quantity = finance.Quantity;
        existingFinance.Type = finance.Type;

        try
        {
            _financeRepository.Update(existingFinance);
            await _unitOfWork.CompleteAsync();

            return new FinanceResponse(existingFinance);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new FinanceResponse($"An error occurred while updating the finance: {e.Message}");
        }

    }

    public async Task<FinanceResponse> DeleteAsync(int financeId)
    {
        var existingFinance = await _financeRepository.FindByIdAsync(financeId);
        
        // Validate Tutorial

        if (existingFinance == null)
            return new FinanceResponse("Finance not found.");
        
        try
        {
            _financeRepository.Remove(existingFinance);
            await _unitOfWork.CompleteAsync();

            return new FinanceResponse(existingFinance);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new FinanceResponse($"An error occurred while deleting the finance: {e.Message}");
        }

    }
}