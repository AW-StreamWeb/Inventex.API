using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services;

public interface IUserService{
    Task<IEnumerable<User>> ListAsync();
    Task<UserResponse> SaveAsync(User user);
    Task<UserResponse> UpdateAsync(int id, User user);
    Task<UserResponse> DeleteAsync(int id);
}