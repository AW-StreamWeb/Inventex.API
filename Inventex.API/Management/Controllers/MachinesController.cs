using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Inventex.API.Shared.Extensions;
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

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveMachineResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var tutorial = _mapper.Map<SaveMachineResource, Machine>(resource);

        var result = await _machineService.SaveAsync(tutorial);

        if (!result.Success)
            return BadRequest(result.Message);

        var machineResource = _mapper.Map<Machine, MachineResource>(result.Resource);

        return Ok(machineResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMachineResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var machine = _mapper.Map<SaveMachineResource, Machine>(resource);

        var result = await _machineService.UpdateAsync(id, machine);

        if (!result.Success)
            return BadRequest(result.Message);

        var machineResource = _mapper.Map<Machine, MachineResource>(result.Resource);

        return Ok(machineResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _machineService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var tutorialResource = _mapper.Map<Machine, MachineResource>(result.Resource);

        return Ok(tutorialResource);
    }
}