using Inventex.API.Management.Domain.Models;
using Inventex.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Resources;

public class MachineResource
{
    [SwaggerSchema("Machine identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Machine Name")]
    public string Name { get; set; }
    [SwaggerSchema("Machine Description")]
    public string Description { get; set; }
    [SwaggerSchema("Machine Lifetime")]
    public string Lifetime { get; set; }
    [SwaggerSchema("Machine Active")]
    public bool Active { get; set; }
    [SwaggerSchema("Machine User identifier")]
    public UserResource User { get; set; }
}