using System;
using System.Collections.Generic;
using TechShop.dao;
using TechShop.entity;

namespace TechShop
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = new ProductService();
            var customerService = new CustomerService();
            var orderService = new OrdersService();
            var reportService = new ReportService(orderService);
            var inventoryService = new InventoryService();
            var paymentService = new PaymentService();
            var searchService = new SearchService();

            Console.WriteLine("Welcome to TechShop!");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("TechShop Management System");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Add Customer");
                Console.WriteLine("4. Place Order");
                Console.WriteLine("5. View Orders");
                Console.WriteLine("6. Add Inventory");
                Console.WriteLine("7. View Inventory");
                Console.WriteLine("8. Sales Report");
                Console.WriteLine("9. Update Customer Info");
                Console.WriteLine("10. Process Payment");
                Console.WriteLine("11. Search Products");
                Console.WriteLine("12. Exit");
                Console.Write("Please select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProduct(productService); break;
                    case "2": ViewProducts(productService); break;
                    case "3": AddCustomer(customerService); break;
                    case "4": PlaceOrder(orderService); break;
                    case "5": ViewOrders(orderService); break;
                    case "6": AddInventory(inventoryService); break;
                    case "7": ViewInventory(inventoryService); break;
                    case "8": GenerateSalesReport(reportService); break;
                    case "9": UpdateCustomerInfo(customerService); break;
                    case "10": ProcessPayment(paymentService); break;
                    case "11": SearchProducts(searchService); break;
                    case "12":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddProduct(ProductService productService)
        {
            Console.WriteLine("Enter Product Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Category: ");
            string category = Console.ReadLine();

            int no_of_times_ordered = 0;
            var product = new Products
            {
                ProductName = name,
                Description = description,
                Price = price,
                No_Of_Times_Ordered = no_of_times_ordered
            };

            productService.AddProduct(product);
            Console.WriteLine("Product added successfully!");
            WaitForKeyPress();
        }

        static void ViewProducts(ProductService productService)
        {
            var products = productService.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine(
                    $"Product ID: {product.ProductID}, " +
                    $"Name: {product.ProductName}, " +
                    $"Description: {product.Description}, " +
                    $"Price: {product.Price}, " +
                    $"Times Ordered: {product.No_Of_Times_Ordered}"
                );
            }
            WaitForKeyPress();
        }

        static void AddCustomer(CustomerService customerService)
        {
            Console.WriteLine("Enter Customer Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Phone: ");
            string phone = Console.ReadLine();

            var customer = new Customers
            {
                CustomerName = name,
                Email = email,
                Phone = phone
            };

            customerService.AddCustomer(customer);
            Console.WriteLine("Customer added successfully!");
            WaitForKeyPress();
        }

        static void PlaceOrder(OrdersService orderService)
        {
            Console.WriteLine("Enter Customer ID: ");
            int customerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Order Date (YYYY-MM-DD): ");
            DateTime orderDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Total Amount: ");
            decimal totalAmount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Order Status: ");
            string status = Console.ReadLine();

            var order = new Orders
            {
                Customer = new Customers { CustomerID = customerId },
                OrderDate = orderDate,
                TotalAmount = totalAmount,
                Status = status
            };

            orderService.AddOrder(order);
            Console.WriteLine("Order placed successfully!");
            WaitForKeyPress();
        }

        static void ViewOrders(OrdersService orderService)
        {
            var orders = orderService.GetAllOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderID}, Customer ID: {order.Customer.CustomerID}, Total Amount: {order.TotalAmount}, Status: {order.Status}");
            }
            WaitForKeyPress();
        }

        static void AddInventory(InventoryService inventoryService)
        {
            Console.WriteLine("Enter Product ID: ");
            int productId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            var inventory = new Inventory
            {
                Product = new Products { ProductID = productId }
            };

            inventory.AddToInventory(quantity);
            inventoryService.AddInventory(inventory);
            Console.WriteLine("Inventory added successfully!");
            WaitForKeyPress();
        }

        static void ViewInventory(InventoryService inventoryService)
        {
            var inventories = inventoryService.GetAllInventory();
            foreach (var inventory in inventories)
            {
                Console.WriteLine($"Product ID: {inventory.Product.ProductID}, Quantity In Stock: {inventory.QuantityInStock}");
            }
            WaitForKeyPress();
        }

        static void GenerateSalesReport(ReportService reportService)
        {
            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            reportService.GenerateSalesReport(startDate, endDate);
            //reportService.GenerateProductSalesReport(startDate, endDate);
            WaitForKeyPress();
        }

        static void UpdateCustomerInfo(CustomerService customerService)
        {
            Console.Write("Enter Customer ID to Update: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Enter new Email: ");
            string newEmail = Console.ReadLine();

            Console.Write("Enter new Phone: ");
            string newPhone = Console.ReadLine();

            bool result = customerService.UpdateCustomerInfo(customerId, newEmail, newPhone);
            Console.WriteLine(result ? "Customer updated successfully!" : "Customer update failed.");
            WaitForKeyPress();
        }

        static void ProcessPayment(PaymentService paymentService)
        {
            Console.Write("Enter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            Console.Write("Enter Payment Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Payment Method (e.g., Credit Card, UPI): ");
            string method = Console.ReadLine();

            var payment = new Payment
            {
                OrderID = orderId,
                Amount = amount,
                PaymentMethod = method,
                PaymentDate = DateTime.Now
            };

            paymentService.ProcessPayment(payment);
            Console.WriteLine("Payment recorded successfully!");
            WaitForKeyPress();
        }

        static void SearchProducts(SearchService searchService)
        {
            Console.Write("Enter keyword to search: ");
            string keyword = Console.ReadLine();

            var results = searchService.SearchProducts(keyword);
            Console.WriteLine("Search Results:");
            foreach (var product in results)
            {
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}");
            }
            WaitForKeyPress();
        }

        static void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
