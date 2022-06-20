namespace Inventex.API.Management.Resources;

public class SaveFinanceResource
{
    public string Name {get; set; }
    public string Day {get; set; }
    public int Quantity {get; set; }
    public bool Type {get; set; }
    public int UserId { get; set; }
}