using Inventex.API.Management.Domain.Models;

namespace Inventex.API.Management.Resources;

public class MachineResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Lifetime { get; set; }
    public bool Active { get; set; }
    public CategoryResource Category { get; set; }
}