using System;
using System.Collections.Generic;
using System.Linq;
using CourierManagementSystem.DAO;
using CourierManagementSystem.Entity;
using CourierManagementSystem.Exception;

using static System.Exception;

namespace Main
{
    class Program
    {
        private static readonly ICourierUserService _userService;
        private static readonly ICourierAdminService _adminService;
        private static readonly CourierServiceDb _dbService;

        static Program()
        {
            var company = new CourierCompanyCollection(
                "FastCourier",
                new System.Collections.Generic.List<Courier>(),
                new System.Collections.Generic.List<Employee>(),
                new System.Collections.Generic.List<Location>()
            );
            _userService = new CourierUserServiceImpl(company);
            _adminService = new CourierAdminServiceCollectionImpl(company);
            _dbService = new CourierServiceDb("db.properties");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Courier Management System...");

            while (true)
            {
                Console.WriteLine("\n=== Courier Management System ===");
                Console.WriteLine("1. Place Order");
                Console.WriteLine("2. Check Order Status");
                Console.WriteLine("3. Cancel Order");
                Console.WriteLine("4. Add Courier Staff (Admin)");
                Console.WriteLine("5. View Delivery History (DB)");
                Console.WriteLine("6. Generate Report (DB)");
                Console.WriteLine("7. Exit");
                Console.Write("Enter choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice)||choice<1||choice>7)
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            PlaceOrder();
                            break;
                        case 2:
                            CheckOrderStatus();
                            break;
                        case 3:
                            CancelOrder();
                            break;
                        case 4:
                            AddCourierStaff();
                            break;
                        case 5:
                            ViewDeliveryHistory();
                            break;
                        case 6:
                            GenerateReport();
                            break;
                        case 7:
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void PlaceOrder()
        {
            Console.Write("Enter User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId) || userId <= 0)
                throw new ArgumentException("Invalid User ID.");

            Console.Write("Enter Sender Name: ");
            string senderName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(senderName))
                throw new ArgumentException("Sender Name is required.");

            Console.Write("Enter Sender Address: ");
            string senderAddress = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(senderAddress))
                throw new ArgumentException("Sender Address is required.");

            Console.Write("Enter Receiver Name: ");
            string receiverName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(receiverName))
                throw new ArgumentException("Receiver Name is required.");

            Console.Write("Enter Receiver Address: ");
            string receiverAddress = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(receiverAddress))
                throw new ArgumentException("Receiver Address is required.");

            Console.Write("Enter Weight: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal weight) || weight <= 0)
                throw new ArgumentException("Invalid Weight.");

            var courier = new Courier
            {
                CourierID = (int)(DateTime.Now.Ticks % 1000000), // Explicitly cast 'long' to 'int'  
                SenderName = senderName,
                SenderAddress = senderAddress,
                ReceiverName = receiverName,
                ReceiverAddress = receiverAddress,
                Weight = (decimal)weight,
                Status = "Processing",
                TrackingNumber = "TRK" + DateTime.Now.Ticks % 1000000,
                DeliveryDate = null,
                UserId = userId
            };

            // Generate TrackingNumber separately since the property is read-only  
            try
            {
                string trackingNumber = _userService.PlaceOrder(courier); // Update in-memory
                _dbService.InsertCourier(courier); // Persist to database
                Console.WriteLine($"Order placed. Tracking Number: {trackingNumber}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void CheckOrderStatus()
        {
            Console.Write("Enter Tracking Number: ");
            string trackingNumber = Console.ReadLine();
            try
            {
                var history = _dbService.GetDeliveryHistory(trackingNumber);
                if (history.Any())
                    Console.WriteLine($"Order Status: {history.First().Status}");
                else
                    Console.WriteLine($"Error: Tracking number {trackingNumber} not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        private static void CancelOrder()
        {
            Console.Write("Enter Tracking Number: ");
            string trackingNumber = Console.ReadLine();
            try
            {
                bool success = _userService.CancelOrder(trackingNumber);
                if (success)
                    Console.WriteLine("Order cancelled successfully.");
                else
                    Console.WriteLine("Order could not be canceled. Check status or tracking number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} | StackTrace: {ex.StackTrace}");
            }
        }

        private static void AddCourierStaff()
        {
            Console.Write("Enter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int empId) || empId <= 0)
                throw new ArgumentException("Invalid Employee ID.");

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Contact Number: ");
            string contact = Console.ReadLine();

            Console.Write("Enter Role: ");
            string role = Console.ReadLine();

            Console.Write("Enter Salary: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary < 0)
                throw new ArgumentException("Invalid Salary.");

            var employee = new Employee
            {
                EmployeeID = empId,
                EmployeeName = name,
                Email = email,
                ContactNumber = contact,
                Role = role,
                Salary = (double)salary
            };

            int id = _adminService.AddCourierStaff(employee);
            Console.WriteLine($"Courier staff added. ID: {id}");
        }

        private static void ViewDeliveryHistory()
        {
            Console.Write("Enter Tracking Number: ");
            string trackingNumber = Console.ReadLine();

            
            try
            {
                var history = _dbService.GetDeliveryHistory(trackingNumber);
                if(history.Any())
                {
                    var courier = history.First();
                    Console.WriteLine($"Courier ID: {courier.CourierID}");
                    Console.WriteLine($"Sender: {courier.SenderName}, {courier.SenderAddress}");
                    Console.WriteLine($"Receiver: {courier.ReceiverName}, {courier.ReceiverAddress}");
                    Console.WriteLine($"Weight: {courier.Weight}, Status: {courier.Status}");
                    Console.WriteLine($"Tracking Number: {courier.TrackingNumber}");
                    Console.WriteLine($"Delivery Date: {courier.DeliveryDate ?? (DateTime?)null}");
                    Console.WriteLine($"User ID: {courier.UserId}");
                }
                else
                {
                    Console.WriteLine($"Error: Tracking number {trackingNumber} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving delivery history: {ex.Message}");
                return;
            }

            
        }

        private static void GenerateReport()
        {
            try
            {
                _dbService.GenerateReport();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
            }
        }
    }
}