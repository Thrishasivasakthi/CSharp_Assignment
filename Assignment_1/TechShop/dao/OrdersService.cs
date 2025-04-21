using Microsoft.Data.SqlClient;
using TechShop.entity;
using TechShop.util;

public class OrdersService
{
    private readonly DatabaseConnector dbConnector = new();

    public void AddOrder(Orders order)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, Status) VALUES (@cid, @date, @amount, @status)", conn);
        cmd.Parameters.AddWithValue("@cid", order.Customer.CustomerID);
        cmd.Parameters.AddWithValue("@date", order.OrderDate);
        cmd.Parameters.AddWithValue("@amount", order.TotalAmount);
        cmd.Parameters.AddWithValue("@status", order.Status);
        cmd.ExecuteNonQuery();
    }

    public void UpdateOrder(Orders order)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("UPDATE Orders SET CustomerID=@cid, OrderDate=@date, TotalAmount=@amount, Status=@status WHERE OrderID=@id", conn);
        cmd.Parameters.AddWithValue("@id", order.OrderID);
        cmd.Parameters.AddWithValue("@cid", order.Customer.CustomerID);
        cmd.Parameters.AddWithValue("@date", order.OrderDate);
        cmd.Parameters.AddWithValue("@amount", order.TotalAmount);
        cmd.Parameters.AddWithValue("@status", order.Status);
        cmd.ExecuteNonQuery();
    }

    public void DeleteOrder(int orderId)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("DELETE FROM Orders WHERE OrderID=@id", conn);
        cmd.Parameters.AddWithValue("@id", orderId);
        cmd.ExecuteNonQuery();
    }

    public List<Orders> GetAllOrders()
    {
        var orders = new List<Orders>();
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("SELECT * FROM Orders", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            orders.Add(new Orders
            {
                OrderID = reader.GetInt32(0),
                Customer = new Customers { CustomerID = reader.GetInt32(1) },
                OrderDate = reader.GetDateTime(2),
                TotalAmount = reader.IsDBNull(3) ? 0m : Convert.ToDecimal(reader[3]),
                Status = reader.GetString(4)
            });
        }
        return orders;
    }

    public DatabaseConnector GetDatabaseConnector()
    {
        return dbConnector;
    }

    public List<Orders> GetOrdersByDateRange(DateTime startDate, DateTime endDate, DatabaseConnector dbConnector)
    {
        var orders = new List<Orders>();

        using (SqlConnection conn = dbConnector.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderDate BETWEEN @Start AND @End", conn))
            {
                cmd.Parameters.AddWithValue("@Start", startDate);
                cmd.Parameters.AddWithValue("@End", endDate);

                // ✅ Make sure the connection is open
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Orders
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            Customer = new Customers { CustomerID = Convert.ToInt32(reader["CustomerID"]) },
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }
        }

        return orders;
    }


    internal IEnumerable<object> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}