using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Resources;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/categories/{categoryId}/machines")]
public class CategoryMachinesController : ControllerBase
{
    private readonly IMachineService _machineService;
    private readonly IMapper _mapper;

    public CategoryMachinesController(IMachineService machineService, IMapper mapper){
        _machineService=machineService;
        _mapper=mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MachineResource>> GetAllByCategoryIdAsync(int categoryId)
    {
        var machines = await _machineService.ListByCategoryIdAsync(categoryId);

        var resources = _mapper.Map<IEnumerable<Machine>, IEnumerable<MachineResource>>(machines);

        return resources;
    }
}