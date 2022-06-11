using AutoMapper;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Management.Resources;
namespace Inventex.API.Management.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile(){
        CreateMap<SaveUserResource, User>();
        CreateMap<SaveMachineResource, Machine>();
        CreateMap<SaveInventoryResource, Inventory>();
    }
}