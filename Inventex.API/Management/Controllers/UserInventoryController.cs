using System.Net.Mime;
using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/inventory")]
[Produces(MediaTypeNames.Application.Json)]
public class UserInventoryController:ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly IMapper _mapper;

    public UserInventoryController(IInventoryService inventoryService, IMapper mapper)
    {
        _inventoryService = inventoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<InventoryResource>> GetAllByUserIdAsync(int userId)
    {
        var inventories = await _inventoryService.ListByUserIdAsync(userId);
        var resources = _mapper.Map < IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);

        return resources;
    } 


}