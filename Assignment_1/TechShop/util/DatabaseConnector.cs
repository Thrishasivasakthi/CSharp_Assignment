using Microsoft.Data.SqlClient;

namespace TechShop.util
{
    public class DatabaseConnector
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection(DatabaseConfig.ConnectionString);
        }
    }
}
