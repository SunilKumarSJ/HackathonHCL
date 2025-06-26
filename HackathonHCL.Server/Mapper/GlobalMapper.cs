using AutoMapper;
using DAL.Entity;
using HackathonHCL.Server.Response;
namespace HackathonHCL.Server.Mapper
{
    public class GlobalMapper:AutoMapper.Profile
    {
        public GlobalMapper() 
        {
            CreateMap<UserList,UserListResponse>();
            CreateMap<User,UserResponse>();
        
        }
    }
}
