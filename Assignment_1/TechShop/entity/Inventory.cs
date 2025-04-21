using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.entity
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public Products Product { get; set; }
        public int QuantityInStock { get; set; }   
        public DateTime LastStockUpdate { get; set; }

        public Products GetProduct() => Product;
        public int GetQuantityInStock() => QuantityInStock;
        public void AddToInventory(int quantity)
        {
            QuantityInStock += quantity;
            LastStockUpdate = DateTime.Now;
        }
        public void RemoveFromInventory(int quantity)
        {
            QuantityInStock -= quantity;
            LastStockUpdate = DateTime.Now;
        }
        public void UpdateStockQuantity(int newQuantity)
        {
            QuantityInStock = newQuantity;
            LastStockUpdate = DateTime.Now;
        }
        public bool IsProductAvailable(int quantityToCheck) => QuantityInStock >= quantityToCheck;
        public decimal GetInventoryValue() => Product.Price * QuantityInStock;

        public static List<Products> ListLowStockProducts(List<Inventory> inventoryList, int threshold)
        {
            return inventoryList
                .Where(inv => inv.QuantityInStock < threshold)
                .Select(inv => inv.Product)
                .ToList();
        }

        public static List<Products> ListOutOfStockProducts(List<Inventory> inventoryList)
        {
            return inventoryList
                .Where(inv => inv.QuantityInStock == 0)
                .Select(inv => inv.Product)
                .ToList();
        }

        public static void ListAllProducts(List<Inventory> inventoryList)
        {
            foreach (var inventory in inventoryList)
            {
                Console.WriteLine($"Product: {inventory.Product.ProductName}, Quantity: {inventory.QuantityInStock}");
            }
        }
    }
}
