using Inventex.API.Management.Domain.Models;

namespace Inventex.API.Management.Domain.Repositories;

public interface IMachineRepository
{
    Task<IEnumerable<Machine>> ListAsync();
    Task AddAsync(Machine machine);
    Task<Machine> FindByIdAsync(int machineId);
    Task<Machine> FindByNameAsync(string name);
    Task<IEnumerable<Machine>> FindByCategoryIdAsync(int categoryId);
    void Update(Machine machine);
    void Remove(Machine machine);
}