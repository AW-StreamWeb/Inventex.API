using AutoMapper;
using Inventex.API.Management.Resources;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Inventex.API.Management.Controllers;

[Route("/api/v1/[controller]")]//ruta en la ejecucion
public class CategoriesController : ControllerBase{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    //DTO
    //ObjectMapping

    public CategoriesController(ICategoryService categoryService, IMapper mapper){//Inyeccion de dependencias x un constructor
        _categoryService=categoryService;
        _mapper=mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryResource>> GetAllAsync(){
        var categories=await _categoryService.ListAsync();
        var resources=_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);//AutoMapper sirve para copiar todos los elementos(mapearlos)

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource){
        if(!ModelState.IsValid){
            return BadRequest(ModelState.GetErrorMessages());
        }
        var category=_mapper.Map<SaveCategoryResource, Category>(resource);
        var result=await _categoryService.SaveAsync(category);
        if(!result.Success){
            return BadRequest(result.Message);
        }
        var CategoryResource= _mapper.Map<Category, CategoryResource>(result.Resource);

        return Ok(CategoryResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource){
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var category = _mapper.Map<SaveCategoryResource, Category>(resource);
        var result = await _categoryService.UpdateAsync(id, category);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);

        return Ok(categoryResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _categoryService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);

        return Ok(categoryResource);
    }
}