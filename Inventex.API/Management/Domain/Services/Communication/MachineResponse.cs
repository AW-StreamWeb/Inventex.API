using Inventex.API.Management.Domain.Models;
using Inventex.API.Shared.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services.Communication;

public class MachineResponse : BaseResponse<Machine>
{
    public MachineResponse(string message) : base(message){

    }
    public MachineResponse(Machine resource) : base(resource){
        
    }
}