using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Resources;

[SwaggerSchema(Required = new[]{"Name"})]
public class SaveMachineResource
{
    [Required]
    [MaxLength(50)]
    [SwaggerSchema("Machine Name")]
    public string Name { get; set; }

    [MaxLength(120)]
    [SwaggerSchema("Machine Description")]
    public string Description { get; set; }

    [MaxLength(20)]
    [SwaggerSchema("Machine Lifetime")]
    public string Lifetime { get; set; }
    
    [SwaggerSchema("Machine Active")]
    public bool Active { get; set; }

    [Required]
    [SwaggerSchema("Machine User Identifier")]
    public int UserId { get; set; }
}