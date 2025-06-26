namespace HackathonHCL.Server.Response
{
    public class ShiftScheduleWithAttendanceResponse
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
