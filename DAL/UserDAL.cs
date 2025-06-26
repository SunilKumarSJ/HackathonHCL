using DAL.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserDAL
    {
        Task<User> Validate(string userName, string password);
        Task<UserList> GetUsers();
        Task<User> GetUserById(int id);
        Task<int> Create(User user);
    }
    public class UserDAL : BaseDAL, IUserDAL
    {
        public UserDAL(IConfiguration configuration) : base(configuration)
        {

        }

        #region Login
        public async Task<User> Validate(string userName, string password)
        {
            User user = null;
            var parms = new SqlParameter[]
            {
                new SqlParameter(){ParameterName ="@UserName",
                    SqlDbType=SqlDbType.NVarChar,
                    Direction=ParameterDirection.Input,
                    Value=userName,
                    IsNullable=false
                },
                new SqlParameter(){ParameterName ="@Password",
                    SqlDbType=SqlDbType.NVarChar,
                    Direction=ParameterDirection.Input,
                    Value=password,
                    IsNullable=false
                }
            };
            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "sp_ValidateLogin";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parms);
                    command.Connection = connection;
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        user = new User()
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = dataReader["Name"].ToString(),
                            UserName = dataReader["UserName"].ToString(),
                            RoleName = dataReader["RoleName"].ToString(),
                            RoleId = Convert.ToInt32(dataReader["RoleId"]),
                            DepartmentId = Convert.ToInt32(dataReader["DepartmentId"])
                        };
                    }
                }
            }
            return user;
        }
        #endregion

        #region Create
        /// <summary>
        ///Create the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> Create(User user)
        {
            int newId = 0;
            var parms = new SqlParameter[]
                    {
                        new SqlParameter(){
                            ParameterName ="@Name",
                            SqlDbType = SqlDbType.NVarChar,
                            IsNullable=true,
                            Value = user.Name,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@UserName",
                            SqlDbType = SqlDbType.NVarChar,
                            IsNullable=true,
                            Value = user.UserName,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@DepartmentId",
                            SqlDbType = SqlDbType.NVarChar,
                            IsNullable=true,
                            Value = user.DepartmentId,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@Password",
                            SqlDbType = SqlDbType.NVarChar,
                            IsNullable=true,
                            Value = user.Password,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@RoleId",
                            SqlDbType = SqlDbType.NVarChar,
                            IsNullable=true,
                            Value = user.RoleId,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@Position",
                            SqlDbType = SqlDbType.NVarChar,
                            IsNullable=true,
                            Value = user.Position,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@ContactNo",
                            SqlDbType = SqlDbType.NVarChar,
                            IsNullable=true,
                            Value = user.ContactNo,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@CreatedBy",
                            SqlDbType = SqlDbType.Int,
                            IsNullable=true,
                            Value = user.CreatedBy,
                            Direction = ParameterDirection.Input,
                        },
                    };
            var outPutParameter = new SqlParameter()
            {
                ParameterName = "@InsertedUserId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "sp_CreateUser";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parms);
                    command.Parameters.Add(outPutParameter);
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                }
            }
            return newId;
        }
        #endregion

        #region GetUsers
        public async Task<UserList> GetUsers()
        {
            UserList userList = new UserList();

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "sp_GetUsers";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        User user = new User()
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = dataReader["Name"].ToString(),
                            UserName = dataReader["UserName"].ToString(),
                            RoleName = dataReader["RoleName"].ToString(),
                            RoleId = Convert.ToInt32(dataReader["RoleId"]),
                            DepartmentId = Convert.ToInt32(dataReader["DepartmentId"]),
                            DepartmentName = dataReader["DepartmentName"].ToString(),
                            Position = dataReader["Position"].ToString(),
                            ContactNo = dataReader["ContactNo"].ToString()
                        };
                        userList.Users.Add(user);
                    }
                }
            }
            return userList;
        }
        #endregion

        #region GetUserById
        public async Task<User> GetUserById(int id)
        {
            User user = null;

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "sp_GetUserById";
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = id,
                        IsNullable = false
                    });
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        user = new User()
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = dataReader["Name"].ToString(),
                            UserName = dataReader["UserName"].ToString(),
                            RoleName = dataReader["RoleName"].ToString(),
                            RoleId = Convert.ToInt32(dataReader["RoleId"]),
                            DepartmentId = Convert.ToInt32(dataReader["DepartmentId"]),
                            DepartmentName = dataReader["DepartmentName"].ToString(),
                            Position = dataReader["Position"].ToString(),
                            ContactNo = dataReader["ContactNo"].ToString()
                        };
                    }
                }
            }
            return user;
        }
        #endregion
    }
}
