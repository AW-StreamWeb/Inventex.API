using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/machines")]
[Produces(MediaTypeNames.Application.Json)]
public class UserMachinesController : ControllerBase
{
    private readonly IMachineService _machineService;
    private readonly IMapper _mapper;

    public UserMachinesController(IMachineService machineService, IMapper mapper){
        _machineService=machineService;
        _mapper=mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Machines for given User",
        Description = "Get existing machines associated with the specified User",
        OperationId = "GetUserMachines",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<MachineResource>> GetAllByUserIdAsync(int userId)
    {
        var machines = await _machineService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Machine>, IEnumerable<MachineResource>>(machines);

        return resources;
    }
}