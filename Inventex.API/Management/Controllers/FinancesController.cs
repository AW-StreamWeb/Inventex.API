using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Inventex.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
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
    public async Task<IEnumerable<FinanceResource>> GetAllAsync()
    {
        var finance = await _financeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Finance>, IEnumerable<FinanceResource>>(finance);
        return resources;
    }

    [HttpPost]
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _financeService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var financeResource = _mapper.Map<Finance, FinanceResource>(result.Resource);

        return Ok(financeResource);
    }
}