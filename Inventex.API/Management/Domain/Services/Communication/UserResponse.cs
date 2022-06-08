using Inventex.API.Management.Domain.Models;
using Inventex.API.Shared.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(string message) : base(message){

        }
        public UserResponse(User resource) : base(resource){
            
        }
    }
}