using DAL.Entity;

namespace HackathonHCL.Server.Response
{

    public class UserResponse
    {//
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string Position { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        public string ContactNo { get; set; } = string.Empty;
    }

    public class UserListResponse
    {
        public UserListResponse()
        {
            this.Users = new List<UserResponse>();
        }
        public List<UserResponse> Users { get; set; }

    }
}
