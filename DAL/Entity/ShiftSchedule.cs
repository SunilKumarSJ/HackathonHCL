using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class ShiftSchedules : BaseEntity
    {//
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ShiftId { get; set; }

        public DateTime ShiftDate { get; set; }

        public int AssignedBy { get; set; }
    }

    public class ShiftScheduleWithAttendance
    {
        public int ScheduleId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? DepartmentName { get; set; }

        public string ShiftName { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime ShiftDate { get; set; }

        public string AttendanceStatus { get; set; } = "Absent";

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }
    }


}
