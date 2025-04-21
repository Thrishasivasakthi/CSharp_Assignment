using System;
using Microsoft.Data.SqlClient;

namespace Banking_System.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetDBConnection()
        {
            string connectionString = DBPropertyUtil.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Database connection established successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error establishing database connection: {ex.Message}");
                throw;
            }

            return connection;
        }
    }
}
