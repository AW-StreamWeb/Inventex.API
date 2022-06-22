using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Resources;

[SwaggerSchema(Required = new []{"Name"})]
public class SaveContactResource
{
    [Required]
    [MaxLength(50)]
    [SwaggerSchema("Contact Name")]
    public string Name { get; set; }

    [MaxLength(120)]
    [SwaggerSchema("Contact Description")]
    public string Description { get; set; }

    [MaxLength(20)]
    [SwaggerSchema("Contact Lifetime")]
    public string Lifetime { get; set; }

    [SwaggerSchema("Contact Active")]
    public bool Active { get; set; }

    [Required]
    [SwaggerSchema("Contact UserId")]
    public int UserId { get; set; }
}