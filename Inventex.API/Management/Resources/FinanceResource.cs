using Inventex.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Resources;

public class FinanceResource
{
    [SwaggerSchema("Finance identifier")]
    public int Id {get; set; }
    [SwaggerSchema("Finance Name")]
    public string Name {get; set; }
    [SwaggerSchema("Finance Day")]
    public string Day {get; set; }
    [SwaggerSchema("Finance Quantity")]
    public int Quantity {get; set; }
    [SwaggerSchema("Finance Type")]
    public bool Type {get; set; }
    
    [SwaggerSchema("Finance User identifier")]
    public UserResource User { get; set; }
}