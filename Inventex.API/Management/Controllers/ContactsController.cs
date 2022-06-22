using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Resources;
using Inventex.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public ContactsController(IContactService contactsService, IMapper mapper){
        _contactService=contactsService;
        _mapper=mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ContactResource>> GetAllAsync(){
        var contact=await _contactService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResource>>(contact);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveContactResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var contact = _mapper.Map<SaveContactResource, Contact>(resource);

        var result = await _contactService.SaveAsync(contact);

        if (!result.Success)
            return BadRequest(result.Message);

        var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);

        return Ok(contactResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveContactResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var contact = _mapper.Map<SaveContactResource, Contact>(resource);

        var result = await _contactService.UpdateAsync(id, contact);

        if (!result.Success)
            return BadRequest(result.Message);

        var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);

        return Ok(contactResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _contactService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);

        return Ok(contactResource);
    }
}