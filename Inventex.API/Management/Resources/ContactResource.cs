using Inventex.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Resources;

public class ContactResource
{
    [SwaggerSchema("Contact identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Contact Name")]
    public string Name { get; set; }
    [SwaggerSchema("Contact Description")]
    public string Description { get; set; }
    [SwaggerSchema("Contact Lifetime")]
    public string Lifetime { get; set; }
    [SwaggerSchema("Contact Active")]
    public bool Active { get; set; }
    [SwaggerSchema("Contact User identifier")]
    public UserResource User { get; set; }
}