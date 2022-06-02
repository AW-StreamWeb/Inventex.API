namespace Inventex.API.Management.Domain.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Relationships

    public IList<Machine> Machines {get; set;}
}