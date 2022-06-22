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
[SwaggerTag("Create, read, update and delete Contacts")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public ContactsController(IContactService contactsService, IMapper mapper){
        _contactService=contactsService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactResource>),statusCode:200)]
    public async Task<IEnumerable<ContactResource>> GetAllAsync(){
        var contact=await _contactService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResource>>(contact);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The contact was successfully created", typeof(ContactResource))]
    [SwaggerResponse(400, "The contact data is not valid")]
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
    [ProducesResponseType(typeof(ContactResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The contact was successfully updated", typeof(ContactResource))]
    [SwaggerResponse(400, "The contact data is not valid")]
    public async Task<IActionResult> PutAsync(int contactId, [FromBody] SaveContactResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var contact = _mapper.Map<SaveContactResource, Contact>(resource);

        var result = await _contactService.UpdateAsync(contactId, contact);

        if (!result.Success)
            return BadRequest(result.Message);

        var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);

        return Ok(contactResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ContactResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The contact was successfully deleted", typeof(ContactResource))]
    [SwaggerResponse(400, "The contact data is not valid")]
    public async Task<IActionResult> DeleteAsync(int contactId)
    {
        var result = await _contactService.DeleteAsync(contactId);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);

        return Ok(contactResource);
    }
}