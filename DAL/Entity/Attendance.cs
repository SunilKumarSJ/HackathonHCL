using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Attendance
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

        public string? Status { get; set; }

        public DateTime? RecordedAt { get; set; }
    }
}
