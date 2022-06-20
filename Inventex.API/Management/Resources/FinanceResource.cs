using Inventex.API.Security.Resources;

namespace Inventex.API.Management.Resources;

public class FinanceResource
{
    public int Id {get; set; }
    public string Name {get; set; }
    public string Day {get; set; }
    public int Quantity {get; set; }
    public bool Type {get; set; }
    
    public UserResource User { get; set; }
}