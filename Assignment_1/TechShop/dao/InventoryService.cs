using TechShop.entity;
using TechShop.util;
using Microsoft.Data.SqlClient;
public class InventoryService
{
    private readonly DatabaseConnector dbConnector = new();

    public DateTime LastStockUpdate { get; private set; }

    public void AddInventory(Inventory inventory)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) VALUES (@pid, @qty, @date)", conn);
        cmd.Parameters.AddWithValue("@pid", inventory.Product.ProductID);
        cmd.Parameters.AddWithValue("@qty", inventory.QuantityInStock);
        cmd.Parameters.AddWithValue("@date", inventory.LastStockUpdate);
        cmd.ExecuteNonQuery();
    }

    public void UpdateInventory(Inventory inventory)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("UPDATE Inventory SET ProductID=@pid, QuantityInStock=@qty, LastStockUpdate=@date WHERE InventoryID=@id", conn);
        cmd.Parameters.AddWithValue("@id", inventory.InventoryID);
        cmd.Parameters.AddWithValue("@pid", inventory.Product.ProductID);
        cmd.Parameters.AddWithValue("@qty", inventory.QuantityInStock);
        cmd.Parameters.AddWithValue("@date", inventory.LastStockUpdate);
        cmd.ExecuteNonQuery();
    }

    public void DeleteInventory(int inventoryId)
    {
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("DELETE FROM Inventory WHERE InventoryID=@id", conn);
        cmd.Parameters.AddWithValue("@id", inventoryId);
        cmd.ExecuteNonQuery();
    }

    public List<Inventory> GetAllInventory()
    {
        return GetAllInventory(LastStockUpdate);
    }

    public List<Inventory> GetAllInventory(DateTime lastStockUpdate)
    {
        var inventoryList = new List<Inventory>();
        using var conn = dbConnector.GetConnection();
        conn.Open();
        SqlCommand cmd = new("SELECT * FROM Inventory", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            inventoryList.Add(new Inventory
            {
                InventoryID = reader.GetInt32(0),
                Product = new Products { ProductID = reader.GetInt32(1) },
                QuantityInStock = reader.GetInt32(2),
                LastStockUpdate = reader.GetDateTime(3)
            });
        }
        return inventoryList;
    }
}
