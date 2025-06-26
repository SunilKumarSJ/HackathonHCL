using BLL;
using HackathonHCL.Server.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackathonHCL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : BaseController
    {
        public readonly IAttendanceBLL attendanceBLL;

        public AttendanceController(IAttendanceBLL attendanceBLL)
        {
            this.attendanceBLL = attendanceBLL;
        }

        [Route("CheckIn")]
        [HttpPut]
        public async Task<BLLResponse> CheckIn(int userId)
        {
            BLLResponse bLLResponse = null;
            var response = await attendanceBLL.CheckIn(userId);
            if (response == 0)
            {
                bLLResponse = CreateSuccessResponse(response, System.Net.HttpStatusCode.OK, "CheckedIn Successfully");
            }
            else
            {
                bLLResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "An error occurred while checkIn");
            }

            return bLLResponse;
        }

        [Route("CheckOut")]
        [HttpPut]
        public async Task<BLLResponse> CheckOut(int userId)
        {
            BLLResponse bLLResponse = null;
            var response = await attendanceBLL.CheckOut(userId);
            if (response == 0)
            {
                bLLResponse = CreateSuccessResponse(response, System.Net.HttpStatusCode.OK, "CheckedOut Successfully");
            }
            else
            {
                bLLResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "An error occurred while checkOut");
            }

            return bLLResponse;
        }
    }
}
