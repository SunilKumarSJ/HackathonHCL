using DAL.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IShiftSchedulesDAL
    {
        Task<int> AssignShiftToUser(int userId, int shiftId, DateTime shiftDate, int assignedBy);
        Task<List<ShiftScheduleWithAttendance>> GetShiftScheduleWithAttendance(DateTime shiftDate);
    }
    public class ShiftSchedulesDAL : BaseDAL, IShiftSchedulesDAL
    {
        public ShiftSchedulesDAL(IConfiguration configuration) : base(configuration)
        {
        }

        #region AssignShiftToUser

        /// <summary>
        /// ss
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="shiftId"></param>
        /// <param name="shiftDate"></param>
        /// <param name="assignedBy"></param>
        /// <returns></returns>
        public async Task<int> AssignShiftToUser(int userId, int shiftId, DateTime shiftDate, int assignedBy)
        {
            int returnValue = -1;
            var parms = new SqlParameter[]
                    {
                        new SqlParameter(){
                            ParameterName ="@UserId",
                            SqlDbType = SqlDbType.Int,
                            IsNullable=true,
                            Value = userId,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@ShiftId",
                            SqlDbType = SqlDbType.Int,
                            IsNullable=true,
                            Value = shiftId,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@ShiftDate",
                            SqlDbType = SqlDbType.Date,
                            IsNullable=true,
                            Value = shiftDate,
                            Direction = ParameterDirection.Input,
                        },
                        new SqlParameter(){
                            ParameterName ="@AssignedBy",
                            SqlDbType = SqlDbType.Int,
                            IsNullable=true,
                            Value = assignedBy,
                            Direction = ParameterDirection.Input,
                        }
                    };
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
                    command.CommandText = "sp_AssignShiftToUser";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parms);
                    command.Parameters.Add(returnParameter);
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    returnValue = (int)command.Parameters["@ReturnVal"].Value;
                }
            }
            return returnValue;
        }

        #endregion

        public async Task<List<ShiftScheduleWithAttendance>> GetShiftScheduleWithAttendance(DateTime shiftDate)
        {
            List<ShiftScheduleWithAttendance> shiftScheduleWithAttendances = new List<ShiftScheduleWithAttendance>();
            ShiftScheduleWithAttendance shiftScheduleWithAttendance = null;
            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "sp_GetShiftScheduleWithAttendance";
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@ShiftDate",
                        SqlDbType = SqlDbType.Date,
                        Direction = ParameterDirection.Input,
                        Value = shiftDate,
                        IsNullable = false
                    });
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        shiftScheduleWithAttendance = new ShiftScheduleWithAttendance()
                        {
                            ScheduleId = Convert.ToInt32(dataReader["ScheduleId"]),
                            UserId = Convert.ToInt32(dataReader["UserId"]),
                            UserName = dataReader["UserName"].ToString(),
                            DepartmentName = dataReader["DepartmentName"].ToString(),
                            ShiftName = dataReader["ShiftName"].ToString(),
                            StartTime = dataReader.GetTimeSpan(dataReader.GetOrdinal("StartTime")),
                            EndTime = dataReader.GetTimeSpan(dataReader.GetOrdinal("EndTime")),
                            ShiftDate = Convert.ToDateTime(dataReader["ShiftDate"]),
                            AttendanceStatus = dataReader["AttendanceStatus"].ToString(),
                            CheckInTime = dataReader.GetTimeSpan(dataReader.GetOrdinal("StartTime")),
                            CheckOutTime = dataReader.GetTimeSpan(dataReader.GetOrdinal("StartTime"))
                        };
                        shiftScheduleWithAttendances.Add(shiftScheduleWithAttendance);
                    }
                }
            }
            return shiftScheduleWithAttendances;
        }
    }
}
