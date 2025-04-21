using System;
using System.Data.SqlClient;

namespace TicketBookingSystem
{
    public class DatabaseConnection
    {
        private static string connectionString = "Server=localhost\\MSSQLSERVER1;Database=TicketBookingSystem;Trusted_Connection=True;";


        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}

