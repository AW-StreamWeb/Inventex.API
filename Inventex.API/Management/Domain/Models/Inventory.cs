namespace Inventex.API.Management.Domain.Models;


public class Inventory
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
        public string InvetoryStatus { get; set; }

        //Relationships 
        
        public User User { get; set; }
    }
