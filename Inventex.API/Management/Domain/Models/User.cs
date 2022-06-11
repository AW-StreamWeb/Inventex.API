namespace Inventex.API.Management.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Relationships

    public IList<Machine> Machines {get; set;}
    public Inventory Inventory {get; set;}
}