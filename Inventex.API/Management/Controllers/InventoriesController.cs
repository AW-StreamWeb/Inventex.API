using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

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
}