using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Inventex.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly IMapper _mapper;

    public InventoriesController(IInventoryService inventoryService, IMapper mapper)
    {
        _inventoryService = inventoryService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<InventoryResource>> GetAllAsync()
    {
        var inventories = await _inventoryService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);
        
        return resources;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveInventoryResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var inventory = _mapper.Map<SaveInventoryResource, Inventory>(resource);

        var result = await _inventoryService.SaveAsync(inventory);

        if (!result.Success)
            return BadRequest(result.Message);

        var inventoryResource = _mapper.Map<Inventory, InventoryResource>(result.Resource);

        return Ok(inventoryResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveInventoryResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var inventory = _mapper.Map<SaveInventoryResource, Inventory>(resource);

        var result = await _inventoryService.UpdateAsync(id, inventory);

        if (!result.Success)
            return BadRequest(result.Message);

        var inventoryResource = _mapper.Map<Inventory, InventoryResource>(result.Resource);

        return Ok(inventoryResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _inventoryService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var inventoryResource = _mapper.Map<Inventory, InventoryResource>(result.Resource);

        return Ok(inventoryResource);
    }
}