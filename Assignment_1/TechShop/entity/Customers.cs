using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.entity
{
    public class Customers
    {
        private int customerId;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private string address;
        private string _email;

        public int CustomerID
        {
            get => customerId;
            set => customerId = value;
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public string Email
        {
            get { return _email; }
            set
            {
                // Simple email validation
                if (!value.Contains("@"))
                {
                    throw new InvalidDataException("Invalid email address format.");
                }
                _email = value;
            }
        }

        public string Phone
        {
            get => phone;
            set => phone = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }
        public object CustomerName { get; internal set; }

        public void CalculateTotalOrders(List<Orders> orders)
        {
            int total = orders.Count(o => o.Customer.CustomerID == this.CustomerID);
            Console.WriteLine($"Total Orders: {total}");
        }

        public void GetCustomerDetails()
        {
            Console.WriteLine($"{FirstName} {LastName}, Email: {Email}, Phone: {Phone}, Address: {Address}");
        }

        public void UpdateCustomerInfo(string newEmail, string newPhone, string newAddress)
        {
            Email = newEmail;
            Phone = newPhone;
            Address = newAddress;
        }
    }
}
