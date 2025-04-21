using System.Data.SqlClient;

namespace assignment_2.util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
