using Inventex.API.Management.Domain.Models;
using Inventex.API.Shared.Domain.Services.Communication;

namespace Inventex.API.Management.Domain.Services.Communication
{
    public class CategoryResponse : BaseResponse<Category>
    {
        public CategoryResponse(string message) : base(message){

        }
        public CategoryResponse(Category resource) : base(resource){
            
        }
    }
}