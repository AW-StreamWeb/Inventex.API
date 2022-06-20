using System.ComponentModel.DataAnnotations;

namespace Inventex.API.Management.Resources;

public class SaveInventoryResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    public byte[] Image { get; set; }
    
    [MaxLength(10)]
    public float Price { get; set; }
    
    [MaxLength(50)]
    public string Category { get; set; }
    
    [MaxLength(10)]
    public int quantity { get; set; }

    [MaxLength(50)]
    public string InventoryStatus { get; set; }
    
    [Required]
    public int UserId { get; set; }
}