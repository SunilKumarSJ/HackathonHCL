namespace HackathonHCL.Server.Request
{
    public class AssignShiftToUser
    {
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int AssignedBy { get; set; }
    }
}
