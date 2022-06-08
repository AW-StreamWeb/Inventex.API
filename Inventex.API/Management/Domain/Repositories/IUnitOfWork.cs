namespace Inventex.API.Management.Domain.Repositories;
public interface IUnitOfWork
{
    Task CompleteAsync();
}
