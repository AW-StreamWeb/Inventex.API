using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services;

public interface ICategoryService{
    Task<IEnumerable<Category>> ListAsync();
    Task<CategoryResponse> SaveAsync(Category category);
    Task<CategoryResponse> UpdateAsync(int id, Category category);
    Task<CategoryResponse> DeleteAsync(int id);
}