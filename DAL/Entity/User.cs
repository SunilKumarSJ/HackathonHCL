using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class User : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string Position { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        public string ContactNo { get; set; } = string.Empty;
    }

    public class UserList
    {
        public UserList() 
        {
          this.Users = new List<User>();
        }
        public List<User> Users { get; set; }

    }
}
