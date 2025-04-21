using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer() { }

        public Customer(int customerId, string customerName, string email, string phoneNumber)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void DisplayCustomerDetails()
        {
            Console.WriteLine($"Customer ID: {CustomerId}, Name: {CustomerName}, Email: {Email}, Phone: {PhoneNumber}");
        }
    }
}
