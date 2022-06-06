using System.ComponentModel.DataAnnotations;

namespace Inventex.API.Management.Resources;

public class SaveMachineResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(120)]
    public string Description { get; set; }

    [MaxLength(20)]
    public string Lifetime { get; set; }

    public bool Active { get; set; }

    [Required]
    public int UserId { get; set; }
}