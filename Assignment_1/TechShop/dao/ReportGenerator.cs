using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.entity;

namespace TechShop.dao
{
    public static class ReportGenerator
    {
        public static void GenerateCustomerPurchaseReport(List<Orders> orders, List<OrderDetails> details)
        {
            Console.WriteLine("=== Customer Purchase Report ===");
            var report = orders.GroupBy(o => o.Customer.CustomerID)
                               .Select(group => new
                               {
                                   CustomerName = group.First().Customer.CustomerName,
                                   TotalSpent = details.Where(d => d.Order.Customer.CustomerID == group.Key)
                                                       .Sum(d => d.CalculateSubtotal())
                               });

            foreach (var entry in report)
            {
                Console.WriteLine($"Customer: {entry.CustomerName}, Total Spent: ${entry.TotalSpent}");
            }
        }

        public static void GenerateProductSalesReport(List<OrderDetails> details)
        {
            Console.WriteLine("\n=== Product Sales Report ===");
            var report = details.GroupBy(d => d.Product.ProductID)
                                .Select(group => new
                                {
                                    ProductName = group.First().Product.ProductName,
                                    TotalUnitsSold = group.Sum(d => d.Quantity),
                                    TotalRevenue = group.Sum(d => d.CalculateSubtotal())
                                });

            foreach (var entry in report)
            {
                Console.WriteLine($"Product: {entry.ProductName}, Units Sold: {entry.TotalUnitsSold}, Revenue: ${entry.TotalRevenue}");
            }
        }

        public static void GenerateInventoryStatusReport(List<Inventory> inventoryList)
        {
            Console.WriteLine("\n=== Inventory Status Report ===");
            foreach (var item in inventoryList)
            {
                Console.WriteLine($"Product: {item.Product.ProductName}, In Stock: {item.QuantityInStock}");
            }
        }
    }
}
