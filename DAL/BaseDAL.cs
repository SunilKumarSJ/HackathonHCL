using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class BaseDAL
    {
        private string connectionstring;
        public BaseDAL(IConfiguration configuration)
        {
            this.connectionstring = configuration.GetConnectionString("HCLHackathonConnectionString");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionstring);
        }
    }
}
