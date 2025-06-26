using AutoMapper;
using BLL;
using HackathonHCL.Server.Request;
using HackathonHCL.Server.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackathonHCL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftSchedulesController : BaseController
    {
        public readonly IShiftSchedulesBLL shiftSchedulesBLL;
        public readonly IMapper mapper;
        public ShiftSchedulesController(IShiftSchedulesBLL shiftSchedulesBLL, IMapper mapper)
        {
            this.shiftSchedulesBLL = shiftSchedulesBLL;
            this.mapper = mapper;
        }

        [Route("GetShiftScheduleWithAttendance")]
        [HttpGet]
        public async Task<BLLResponse> GetShiftScheduleWithAttendance(DateTime shiftDate)
        {
            BLLResponse bllResponse = null;
            var response = await shiftSchedulesBLL.GetShiftScheduleWithAttendance(shiftDate);
            if (response != null && response.Count > 0)
            {
                bllResponse = CreateSuccessResponse(response, System.Net.HttpStatusCode.OK);
            }
            else
            {
                bllResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "An error occured while fetching data");
            }

            return bllResponse;
        }

        [Route("AssignShiftToUser")]
        [HttpPost]
        public async Task<BLLResponse> AssignShiftToUser(int userId, int shiftId, DateTime shiftDate, int assignedBy)
        {
            BLLResponse bllResponse = null;
            var response = await shiftSchedulesBLL.AssignShiftToUser(userId, shiftId, shiftDate, assignedBy);
            if (response == 0)
            {
                bllResponse = CreateSuccessResponse(response, System.Net.HttpStatusCode.OK, "Shift assigned to user successfully");
            }
            else if (response == 1)
            {
                bllResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "Shift is already assign to the user.");
            }
            else
            {
                bllResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "An error occurred while assigning shift");
            }

            return bllResponse;
        }
    }
}
