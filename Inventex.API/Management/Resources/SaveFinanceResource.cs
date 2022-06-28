using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Resources;

[SwaggerSchema(Required = new []{"Name"})]
public class SaveFinanceResource
{
    [Required]
    [MaxLength(50)]
    [SwaggerSchema("Finance Name")]
    public string Name {get; set; }
    
    [MaxLength(20)]
    [SwaggerSchema("Finance Day")]
    public string Day {get; set; }
    
    [SwaggerSchema("Finance Quantity")]
    public int Quantity {get; set; }
    
    [SwaggerSchema("Finance Type")]
    public bool Type {get; set; }
    
    [Required]
    [SwaggerSchema("Finance User Identifier")]
    public int UserId { get; set; }
}