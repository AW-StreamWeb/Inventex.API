using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;


[ApiController]
[Route("/api/v1/users/{userId}/contacts")]
public class UserContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public UserContactsController(IContactService contactService, IMapper mapper){
        _contactService=contactService;
        _mapper=mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ContactResource>> GetAllByUserIdAsync(int userId)
    {
        var contacts = await _contactService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResource>>(contacts);

        return resources;
    }
}