using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.entity
{
    public class Products
    {
        private int productId;
        private string productName;
        private string description;
        private decimal price;

        public int ProductID { get => productId; set => productId = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string Description { get => description; set => description = value; }
        public int No_Of_Times_Ordered { get; set; }

        public decimal Price
        {
            get => price;
            set
            {
                if (value < 0) throw new InvalidDataException("Price cannot be negative.");
                price = value;
            }
        }

        public void GetProductDetails()
        {
            Console.WriteLine($"{ProductName} - {Description}, Price: ${Price}");
        }

        public void UpdateProductInfo(string desc, decimal newPrice)
        {
            Description = desc;
            Price = newPrice;
        }

        public bool IsProductInStock(Inventory inventory)
        {
            return inventory.QuantityInStock > 0;
        }
    }
}
