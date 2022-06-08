namespace Inventex.API.Management.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Info Profile
    public string Phone { get; set; }
    public string Country { get; set; }
    public string About { get; set; }
    //Relationships

    public IList<Machine> Machines {get; set;}
}