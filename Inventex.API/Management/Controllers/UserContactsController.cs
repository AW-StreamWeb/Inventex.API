using System.Net.Mime;
using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Inventex.API.Management.Controllers;


[ApiController]
[Route("/api/v1/users/{userId}/contacts")]
[Produces(MediaTypeNames.Application.Json)]
public class UserContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public UserContactsController(IContactService contactService, IMapper mapper){
        _contactService=contactService;
        _mapper=mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Contacts for given User",
        Description = "Get existing contacts associated with the specified User",
        OperationId = "GetUserContacts",
        Tags = new []{"Users"}
        )]
    public async Task<IEnumerable<ContactResource>> GetAllByUserIdAsync(int userId)
    {
        var contacts = await _contactService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResource>>(contacts);

        return resources;
    }
}