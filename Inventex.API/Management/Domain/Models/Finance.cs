using Inventex.API.Security.Domain.Models;

namespace Inventex.API.Management.Domain.Models
{
    public class Finance
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Day {get; set; }
        public int Quantity {get; set; }
        public bool Type {get; set; }
        
        //Relationships

        public int UserId { get; set; }
        public User User {get; set; }
    }
}