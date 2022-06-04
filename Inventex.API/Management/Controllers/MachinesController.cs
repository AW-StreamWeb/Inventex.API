using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MachinesController : ControllerBase
{
    private readonly IMachineService _machineService;
    private readonly IMapper _mapper;

    public MachinesController(IMachineService machineService, IMapper mapper){
        _machineService=machineService;
        _mapper=mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MachineResource>> GetAllAsync(){
        var machine=await _machineService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Machine>, IEnumerable<MachineResource>>(machine);

        return resources;
    }
}