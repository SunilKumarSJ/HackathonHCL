using DAL;
using DAL.Entity;

namespace BLL
{
    public interface IUserBLL
    {//
        Task<User> Validate(string userName, string password);
        Task<UserList> GetUsers();
        Task<User> GetUserById(int id);
        Task<int> CreateUser(User user);
    }
    public class UserBLL: IUserBLL
    {
        public readonly IUserDAL userDAL;
        public UserBLL(IUserDAL userDAL)
        {
            this.userDAL = userDAL;
        }

        // Validate user credentials
        public async Task<User> Validate(string userName, string password)
        {
            return await userDAL.Validate(userName, password);
        }

        // Get all users
        public async Task<UserList> GetUsers()
        {
            return await userDAL.GetUsers();
        }

        // Get single user by Id
        public async Task<User> GetUserById(int id)
        {
            return await userDAL.GetUserById(id);
        }

        // Create new user
        public async Task<int> CreateUser(User user)
        {
            return await userDAL.Create(user);
        }
    }
}
