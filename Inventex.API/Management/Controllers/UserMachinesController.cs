using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Resources;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/machines")]
public class UserMachinesController : ControllerBase
{
    private readonly IMachineService _machineService;
    private readonly IMapper _mapper;

    public UserMachinesController(IMachineService machineService, IMapper mapper){
        _machineService=machineService;
        _mapper=mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MachineResource>> GetAllByUserIdAsync(int categoryId)
    {
        var machines = await _machineService.ListByUserIdAsync(categoryId);

        var resources = _mapper.Map<IEnumerable<Machine>, IEnumerable<MachineResource>>(machines);

        return resources;
    }
}