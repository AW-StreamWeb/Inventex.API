using Inventex.API.Management.Domain.Models;

namespace Inventex.API.Management.Domain.Repositories;

public interface IFinanceRepository
{
    Task<IEnumerable<Finance>> ListAsync();
    Task AddAsync(Finance finance);
    Task<Finance> FindByIdAsync(int Id);
    Task<Finance> FindByNameAsync(string name);
    Task<IEnumerable<Finance>> FindByUserIdAsync(int userId);
    void Update(Finance finance);
    void Remove(Finance finance);
    
}