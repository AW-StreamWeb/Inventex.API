using Inventex.API.Management.Domain.Models;
using Inventex.API.Shared.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services.Communication;

public class FinanceResponse : BaseResponse<Finance>
{
    public FinanceResponse(string message) : base(message){

    }
    public FinanceResponse(Finance resource) : base(resource){
        
    }
}