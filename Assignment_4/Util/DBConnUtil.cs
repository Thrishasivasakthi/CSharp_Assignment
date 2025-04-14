using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace CourierManagementSystem.Util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection(string propFile)
        {
            string connStr = DBPropertyUtil.GetConnectionString(propFile);
            if (string.IsNullOrEmpty(connStr))
                throw new InvalidOperationException("Connection string is invalid or empty.");
            return new SqlConnection(connStr);
        }
    }
}
