using System.Net.Mime;
using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/finances")]
[Produces(MediaTypeNames.Application.Json)]

public class UserFinancesController:ControllerBase
{
    private readonly IFinanceService _financeService;
    private readonly IMapper _mapper;


    public UserFinancesController(IFinanceService financeService, IMapper mapper)
    {
        _financeService = financeService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Finances for given User",
        Description = "Get existing finances associated with the specified User",
        OperationId = "GetUserFinances",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<FinanceResource>> GetAllByUserIdAsync(int userId)
    {
        var finances = await _financeService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Finance>, IEnumerable<FinanceResource>>(finances);

        return resources;
    }
}