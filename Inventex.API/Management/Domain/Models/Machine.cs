namespace Inventex.API.Management.Domain.Models
{
    public class Machine
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Description {get; set; }
        public string Lifetime {get; set; }
        public bool Active {get; set; }

        //Relationships

        public int CategoryId { get; set; }
        public Category Category {get; set; }
    }
}