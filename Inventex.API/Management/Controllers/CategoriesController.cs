using AutoMapper;
using Inventex.API.Management.Resources;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Domain.Services;
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
}