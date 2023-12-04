using Ecom.API.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController:BaseAPIController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}