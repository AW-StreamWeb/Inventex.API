using System.Net.Mime;
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
[SwaggerTag("Created, read, update and delete Finances")]
public class FinancesController:ControllerBase
{
    private readonly IFinanceService _financeService;
    private readonly IMapper _mapper;

    public FinancesController(IFinanceService financeService, IMapper mapper)
    {
        _financeService = financeService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FinanceResource>), statusCode:200)]
    public async Task<IEnumerable<FinanceResource>> GetAllAsync()
    {
        var finance = await _financeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Finance>, IEnumerable<FinanceResource>>(finance);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(FinanceResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The finance was successfully created.", typeof(MachineResource))]
    [SwaggerResponse(400, "The finance data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveFinanceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var finance = _mapper.Map<SaveFinanceResource, Finance>(resource);
        var result = await _financeService.SaveAsync(finance);

        if (!result.Success)
            return BadRequest(result.Message);

        var financeResource = _mapper.Map<Finance, FinanceResource>(result.Resource);
        return Ok(financeResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FinanceResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The finance was successfully updated.", typeof(MachineResource))]
    [SwaggerResponse(400, "The finance data is not valid")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFinanceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var finance = _mapper.Map<SaveFinanceResource, Finance>(resource);
        var result = await _financeService.UpdateAsync(id, finance);

        if (!result.Success)
            return BadRequest(result.Message);

        var financeResource = _mapper.Map<Finance, FinanceResource>(result.Resource);

        return Ok(financeResource);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(FinanceResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The finance was successfully deleted.", typeof(MachineResource))]
    [SwaggerResponse(400, "The finance data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _financeService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var financeResource = _mapper.Map<Finance, FinanceResource>(result.Resource);

        return Ok(financeResource);
    }
}