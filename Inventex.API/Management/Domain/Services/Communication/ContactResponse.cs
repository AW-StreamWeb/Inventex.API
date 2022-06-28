using Inventex.API.Management.Domain.Models;
using Inventex.API.Shared.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services.Communication;

public class ContactResponse :  BaseResponse<Contact>
{
    public ContactResponse(string message) : base(message){

    }
    public ContactResponse(Contact resource) : base(resource){
        
    }
}