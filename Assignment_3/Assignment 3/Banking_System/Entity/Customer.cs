using System;
using System.Text.RegularExpressions;

namespace Banking_System.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Default constructor
        public Customer() { }

        // Overloaded constructor
        public Customer(int customerId, string firstName, string lastName, string email, string phoneNumber, string address)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9]+[.]*[a-zA-Z0-9]*@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 10 && long.TryParse(phoneNumber, out _);
        }

        public void PrintCustomerInfo()
        {
            Console.WriteLine($"Customer ID: {CustomerId}");
            Console.WriteLine($"Full Name: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {PhoneNumber}");
            Console.WriteLine($"Address: {Address}");
        }
    }
}
