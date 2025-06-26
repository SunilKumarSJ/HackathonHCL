using BLL.Helper;
using BLL;
using HackathonHCL.Server.Request;
using HackathonHCL.Server.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AutoMapper;
using DAL.Entity;

namespace HackathonHCL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public readonly IOptions<JWTSetting> appSettings;
        public readonly IUserBLL userBLL;
        public readonly ITokenBLL tokenBLL;
        public readonly IMapper mapper;
        public UserController(IUserBLL userBLL, ITokenBLL tokenBLL, IOptions<JWTSetting> appSettings, IMapper mapper)
        {
            this.appSettings = appSettings;
            this.userBLL = userBLL;
            this.tokenBLL = tokenBLL;
            this.mapper = mapper;
        }

        #region Login
        /// <summary>
        /// //
        /// </summary>
        /// <param name="authModel"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<BLLResponse> Login(AuthRequest authModel)
        {
            BLLResponse bLLResponse = null;
            int retVal = 0;
            string token = null;
            try
            {
                //var password = EncryDecry.EnryptString(authModel.Password);
                var expires = appSettings.Value.TokenExpiryTimeInMinutes;
                var user = await userBLL.Validate(authModel.UserName, authModel.Password);

                if (user == null)
                {
                    bLLResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "Please enter valid credebtials");
                }
                if (user != null)
                {
                    token = tokenBLL.GenerateToken(user, DateTime.UtcNow.AddMinutes(Convert.ToInt32(expires)));
                    bLLResponse = CreateSuccessResponse(token, System.Net.HttpStatusCode.OK, "User loggedIn Successfully");
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bLLResponse;

        }

        #endregion

        #region GetAll
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authModel"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<BLLResponse> GetAll()
        {
            BLLResponse bLLResponse = null;
            try
            {
                var users = await userBLL.GetUsers();
                var userListResponse = mapper.Map<UserList, UserListResponse>(users);
                if (userListResponse == null || userListResponse.Users.Count == 0)
                {
                    bLLResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "An error occured while reading users");
                }
                else
                {
                    bLLResponse = CreateSuccessResponse(userListResponse.Users, System.Net.HttpStatusCode.OK);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bLLResponse;

        }

        #endregion

        #region GetById
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetById")]
        [HttpGet]
        public async Task<BLLResponse> GetById(int id)
        {
            BLLResponse bLLResponse = null;
            try
            {
                var user = await userBLL.GetUserById(id);
                var userResponse = mapper.Map<User, UserResponse>(user);
                if (userResponse == null)
                {
                    bLLResponse = CreateFailResponse(null, System.Net.HttpStatusCode.InternalServerError, "An error occured while reading users");
                }
                else
                {
                    bLLResponse = CreateSuccessResponse(userResponse, System.Net.HttpStatusCode.OK);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bLLResponse;

        }

        #endregion
    }


}
