using System.Net.Mime;
using System.Security.Permissions;
using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Inventex.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Machines")]
public class MachinesController : ControllerBase
{
    private readonly IMachineService _machineService;
    private readonly IMapper _mapper;

    public MachinesController(IMachineService machineService, IMapper mapper){
        _machineService=machineService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MachineResource>), statusCode:200)]
    public async Task<IEnumerable<MachineResource>> GetAllAsync(){
        var machine=await _machineService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Machine>, IEnumerable<MachineResource>>(machine);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MachineResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The machine was successfully created.", typeof(MachineResource))]
    [SwaggerResponse(400, "The machine data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveMachineResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var machine = _mapper.Map<SaveMachineResource, Machine>(resource);

        var result = await _machineService.SaveAsync(machine);

        if (!result.Success)
            return BadRequest(result.Message);

        var machineResource = _mapper.Map<Machine, MachineResource>(result.Resource);

        return Created(nameof(PostAsync), machineResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(MachineResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The machine was successfully updated.", typeof(MachineResource))]
    [SwaggerResponse(400, "The machine data is not valid")]
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
    [ProducesResponseType(typeof(MachineResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The machine was successfully deleted.", typeof(MachineResource))]
    [SwaggerResponse(400, "The machine data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _machineService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var machineResource = _mapper.Map<Machine, MachineResource>(result.Resource);

        return Ok(machineResource);
    }
}