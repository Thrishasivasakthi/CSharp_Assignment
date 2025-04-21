using Microsoft.Data.SqlClient;
using TechShop.entity;
using TechShop.util;

public class OrderDetailsService
{
    private readonly DatabaseConnector dbConnector = new();

    public void AddOrderDetail(OrderDetails detail)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES (@oid, @pid, @qty)", conn);
        cmd.Parameters.AddWithValue("@oid", detail.Order.OrderID);
        cmd.Parameters.AddWithValue("@pid", detail.Product.ProductID);
        cmd.Parameters.AddWithValue("@qty", detail.Quantity);
        cmd.ExecuteNonQuery();
    }

    public void UpdateOrderDetail(OrderDetails detail)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("UPDATE OrderDetails SET OrderID=@oid, ProductID=@pid, Quantity=@qty WHERE OrderDetailID=@id", conn);
        cmd.Parameters.AddWithValue("@id", detail.OrderDetailID);
        cmd.Parameters.AddWithValue("@oid", detail.Order.OrderID);
        cmd.Parameters.AddWithValue("@pid", detail.Product.ProductID);
        cmd.Parameters.AddWithValue("@qty", detail.Quantity);
        cmd.ExecuteNonQuery();
    }

    public void DeleteOrderDetail(int orderDetailId)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("DELETE FROM OrderDetails WHERE OrderDetailID=@id", conn);
        cmd.Parameters.AddWithValue("@id", orderDetailId);
        cmd.ExecuteNonQuery();
    }

    public List<OrderDetails> GetAllOrderDetails()
    {
        var details = new List<OrderDetails>();
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("SELECT * FROM OrderDetails", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            details.Add(new OrderDetails
            {
                OrderDetailID = reader.GetInt32(0),
                Order = new Orders { OrderID = reader.GetInt32(1) },
                Product = new Products { ProductID = reader.GetInt32(2) },
                Quantity = reader.GetInt32(3)
            });
        }
        return details;
    }
}