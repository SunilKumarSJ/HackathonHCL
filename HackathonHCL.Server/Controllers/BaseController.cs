using HackathonHCL.Server.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HackathonHCL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected BLLResponse CreateSuccessResponse(object response, HttpStatusCode httpStatusCode = HttpStatusCode.OK, string message = "Success")
        {
            return new BLLResponse()
            {
                Response = response,
                StatusCode = (int)httpStatusCode,
                Message = message,
                Status = true
            };
        }

        [NonAction]
        protected BLLResponse CreateFailResponse(object response, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string errorMessage = "Failed")
        {
            return new BLLResponse()
            {
                Response = response,
                StatusCode = (int)httpStatusCode,
                ErrorMessage = errorMessage,
                Status = false
            };
        }
    }
}
