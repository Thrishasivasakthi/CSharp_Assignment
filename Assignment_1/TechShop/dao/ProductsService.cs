using Microsoft.Data.SqlClient;
using TechShop.entity;
using TechShop.util;

public class ProductService
{
    private readonly DatabaseConnector dbConnector = new();

    public void AddProduct(Products product)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("INSERT INTO Products (ProductName, Description, Price, No_Of_Times_Ordered) VALUES (@name, @desc, @price, @times)", conn);
        cmd.Parameters.AddWithValue("@name", product.ProductName);
        cmd.Parameters.AddWithValue("@desc", product.Description);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.Parameters.AddWithValue("@times", product.No_Of_Times_Ordered);
        cmd.ExecuteNonQuery();
    }

    public void UpdateProduct(Products product)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("UPDATE Products SET ProductName=@name, Description=@desc, Price=@price, No_Of_Times_Ordered=@times WHERE ProductID=@id", conn);
        cmd.Parameters.AddWithValue("@id", product.ProductID);
        cmd.Parameters.AddWithValue("@name", product.ProductName);
        cmd.Parameters.AddWithValue("@desc", product.Description);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.Parameters.AddWithValue("@times", product.No_Of_Times_Ordered);
        cmd.ExecuteNonQuery();
    }

    public void DeleteProduct(int productId)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("DELETE FROM Products WHERE ProductID=@id", conn);
        cmd.Parameters.AddWithValue("@id", productId);
        cmd.ExecuteNonQuery();
    }

    public List<Products> GetAllProducts()
    {
        var products = new List<Products>();
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("SELECT * FROM Products", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            products.Add(new Products
            {
                ProductID = reader.GetInt32(0),
                ProductName = reader.GetString(1),
                Description = reader.GetString(2),
                Price = reader.IsDBNull(3) ? 0m : Convert.ToDecimal(reader[3]),
                No_Of_Times_Ordered = reader.GetInt32(4)
            });
        }
        return products;
    }
}
