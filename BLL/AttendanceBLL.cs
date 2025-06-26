using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IAttendanceBLL
    {
        Task<int> CheckIn(int userId);
        Task<int> CheckOut(int userId);
    }
    public class AttendanceBLL : IAttendanceBLL
    {
        public readonly IAttendanceDAL attendanceDAL;
        public AttendanceBLL(IAttendanceDAL attendanceDAL)
        {
            this.attendanceDAL = attendanceDAL;
        }

        public async Task<int> CheckIn(int userId)
        {
            return await attendanceDAL.CheckIn(userId);
        }

        public async Task<int> CheckOut(int userId)
        {
            return await attendanceDAL.CheckOut(userId);
        }
    }
}
