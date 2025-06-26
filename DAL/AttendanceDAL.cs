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
    public interface IAttendanceDAL
    {
        Task<int> CheckIn(int userId);
        Task<int> CheckOut(int userId);
    }
    public class AttendanceDAL : BaseDAL, IAttendanceDAL
    {
        public AttendanceDAL(IConfiguration configuration) : base(configuration)
        {
        }

        #region CheckIn

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> CheckIn(int userId)
        {
            int returnValue = -1;


            var returnParameter = new SqlParameter()
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue,
            };

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "sp_CheckInAttendance";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@UserID",
                        SqlDbType = SqlDbType.Int,
                        IsNullable = true,
                        Value = userId,
                        Direction = ParameterDirection.Input,
                    });
                    command.Parameters.Add(returnParameter);
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    returnValue = (int)command.Parameters["@ReturnVal"].Value;
                }
            }
            return returnValue;
        }

        #endregion

        #region CheckOut

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> CheckOut(int userId)
        {
            int returnValue = -1;


            var returnParameter = new SqlParameter()
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue,
            };

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "sp_CheckOutAttendance";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@UserID",
                        SqlDbType = SqlDbType.Int,
                        IsNullable = true,
                        Value = userId,
                        Direction = ParameterDirection.Input,
                    });
                    command.Parameters.Add(returnParameter);
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    returnValue = (int)command.Parameters["@ReturnVal"].Value;
                }//
            }
            return returnValue;
        }

        #endregion
    }
}
