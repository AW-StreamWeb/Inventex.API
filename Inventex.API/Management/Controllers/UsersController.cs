using AutoMapper;
using Inventex.API.Management.Resources;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

[Route("/api/v1/[controller]")]//ruta en la ejecucion
public class UsersController : ControllerBase{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    //DTO
    //ObjectMapping

    public UsersController(IUserService userService, IMapper mapper){//Inyeccion de dependencias x un constructor
        _userService=userService;
        _mapper=mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<UserResource>> GetAllAsync(){
        var categories=await _userService.ListAsync();
        var resources=_mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(categories);//AutoMapper sirve para copiar todos los elementos(mapearlos)

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource){
        if(!ModelState.IsValid){
            return BadRequest(ModelState.GetErrorMessages());
        }
        var category=_mapper.Map<SaveUserResource, User>(resource);
        var result=await _userService.SaveAsync(category);
        if(!result.Success){
            return BadRequest(result.Message);
        }
        var categoryResource= _mapper.Map<User, UserResource>(result.Resource);

        return Ok(categoryResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource){
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var user = _mapper.Map<SaveUserResource, User>(resource);
        var result = await _userService.UpdateAsync(id, user);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var userResource = _mapper.Map<User, UserResource>(result.Resource);

        return Ok(userResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var userResource = _mapper.Map<User, UserResource>(result.Resource);

        return Ok(userResource);
    }
}