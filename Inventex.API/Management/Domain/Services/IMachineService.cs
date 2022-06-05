using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services;

public interface IMachineService
{
    Task<IEnumerable<Machine>> ListAsync();
    Task<IEnumerable<Machine>> ListByUserIdAsync(int userId);
    Task<MachineResponse> SaveAsync(Machine machine);
    Task<MachineResponse> UpdateAsync(int machineId, Machine machine);
    Task<MachineResponse> DeleteAsync(int machineId);
}