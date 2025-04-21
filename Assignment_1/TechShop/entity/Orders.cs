using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.entity
{
    public class Orders
    {
        private int orderId;
        private Customers customer;
        private DateTime orderDate;
        private decimal totalAmount;
        private string status;
        private int version; // Version field to track changes for optimistic concurrency

        public int OrderID { get => orderId; set => orderId = value; }
        public Customers Customer { get => customer; set => customer = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public decimal TotalAmount { get => totalAmount; set => totalAmount = value; }
        public string Status { get => status; set => status = value; }
        public int Version { get => version; set => version = value; } // Version property for concurrency control

        // Method to calculate total amount for the order
        public void CalculateTotalAmount(List<OrderDetails> details)
        {
            TotalAmount = details.Where(d => d.Order.OrderID == OrderID).Sum(d => d.CalculateSubtotal());
        }

        // Method to get details of the order
        public void GetOrderDetails(List<OrderDetails> details)
        {
            Console.WriteLine($"Order ID: {OrderID}, Date: {OrderDate}, Status: {Status}, Version: {Version}");
            foreach (var detail in details.Where(d => d.Order.OrderID == OrderID))
                detail.GetOrderDetailInfo();
        }

        // Method to update the order status, with optimistic concurrency handling
        public void UpdateOrderStatus(string newStatus, int currentVersion)
        {
            if (currentVersion != Version)
            {
                // If the version doesn't match, throw an exception indicating concurrency conflict
                throw new OptimisticConcurrencyException("Concurrency conflict: The order has already been updated by another user.");
            }

            // If version matches, proceed with the update
            Status = newStatus;
            Version++; // Increment the version to indicate a new update
            Console.WriteLine($"Order status updated to '{newStatus}' (Version {Version}).");
        }

        // Method to handle the payment and check for failures
        public void ProcessPayment(decimal paymentAmount)
        {
            try
            {
                // Instantiate the payment processor
                PaymentProcessing paymentProcessor = new PaymentProcessing();

                // 'this' refers to the current Order object (assuming this method is inside the Orders class)
                paymentProcessor.ProcessPayment(this, paymentAmount);

                // Update the order status
                Status = "Paid";
                Console.WriteLine($"Payment successful for Order ID: {OrderID}. Order status updated to 'Paid'.");
            }
            catch (PaymentFailedException ex)
            {
                // Update the order status on failure
                Status = "Payment Failed";
                Console.WriteLine($"Payment failed for Order ID: {OrderID}. Reason: {ex.Message}");
            }
        }


        // Method to cancel the order and return items to inventory
        public void CancelOrder(List<OrderDetails> details, List<Inventory> inventoryList)
        {
            Status = "Cancelled";
            foreach (var detail in details.Where(d => d.Order.OrderID == OrderID))
            {
                var inv = inventoryList.FirstOrDefault(i => i.Product.ProductID == detail.Product.ProductID);
                inv?.AddToInventory(detail.Quantity);
            }
        }

        internal void UpdateOrderStatus(string newStatus)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<object> GetOrderDetails()
        {
            throw new NotImplementedException();
        }
    }

    // Custom Optimistic Concurrency Exception class
    public class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException(string message) : base(message) { }
    }

    // Payment Processing class (same as before)
    public class PaymentProcessing
    {
        // Retain only one of the ProcessPayment methods. 
        // Here, we remove the internal method to avoid ambiguity.

        public void ProcessPayment(Orders order, decimal amount)
        {
            bool isPaymentSuccessful = ProcessTransaction(order, amount);

            if (!isPaymentSuccessful)
            {
                throw new PaymentFailedException("Payment processing failed. Please check your payment details or try again.");
            }

            // If payment is successful, update order status
            Console.WriteLine("Payment processed successfully.");
        }

        // Remove the duplicate internal method to resolve ambiguity.
        // internal void ProcessPayment(Orders orders, decimal paymentAmount)
        // {
        //     throw new NotImplementedException();
        // }

        private bool ProcessTransaction(Orders order, decimal amount)
        {
            // Simulate payment transaction logic
            if (amount <= 0 || order.TotalAmount != amount)
            {
                return false; // Simulate payment failure
            }
            return true; // Simulate successful payment
        }
    }

    // Custom Payment Failed Exception class (same as before)
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException(string message) : base(message) { }
    }
}
