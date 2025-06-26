using DAL;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IShiftSchedulesBLL
    {
        Task<int> AssignShiftToUser(int userId, int shiftId, DateTime shiftDate, int assignedBy);
        Task<List<ShiftScheduleWithAttendance>> GetShiftScheduleWithAttendance(DateTime shiftDate);
    }
    public class ShiftSchedulesBLL : IShiftSchedulesBLL
    {
        public readonly IShiftSchedulesDAL shiftSchedulesDAL;

        public ShiftSchedulesBLL(IShiftSchedulesDAL shiftSchedulesDAL)
        {
            this.shiftSchedulesDAL = shiftSchedulesDAL;
        }

        public Task<int> AssignShiftToUser(int userId, int shiftId, DateTime shiftDate, int assignedBy)
        {
            return shiftSchedulesDAL.AssignShiftToUser(userId, shiftId, shiftDate, assignedBy);
        }

        public Task<List<ShiftScheduleWithAttendance>> GetShiftScheduleWithAttendance(DateTime shiftDate)
        {
            return shiftSchedulesDAL.GetShiftScheduleWithAttendance(shiftDate);
        }
    }
}
