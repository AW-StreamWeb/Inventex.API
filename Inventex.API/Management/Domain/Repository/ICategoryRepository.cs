using Inventex.API.Management.Domain.Models;

namespace Inventex.API.Management.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> ListAsync();
    Task AddAsync(Category category);
    Task<Category> FindIdAsync(int id);
    void Update(Category category);
    void Remove(Category category);
}