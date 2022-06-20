using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services;

public interface IFinanceService
{
    Task<IEnumerable<Finance>> ListAsync();
    Task<IEnumerable<Finance>> ListByUserIdAsync(int userId);
    Task<FinanceResponse> SaveAsync(Finance finance);
    Task<FinanceResponse> UpdateAsync(int id, Finance finance);
    Task<FinanceResponse> DeleteAsync(int id);
}