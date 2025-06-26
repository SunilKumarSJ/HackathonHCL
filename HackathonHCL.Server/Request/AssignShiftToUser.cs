namespace HackathonHCL.Server.Request
{
    public class AssignShiftToUser
    {
        // int userId, int shiftId, DateTime shiftDate, int assignedBy

        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int AssignedBy { get; set; }
    }
}
