using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TechShop.entity;
using TechShop.util;

namespace TechShop.dao
{
    public class CustomerService
    {
        private readonly DatabaseConnector dbConnector = new();

        public void AddCustomer(Customers customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName) ||
                string.IsNullOrWhiteSpace(customer.LastName) ||
                string.IsNullOrWhiteSpace(customer.Email) ||
                string.IsNullOrWhiteSpace(customer.Phone))
            {
                Console.WriteLine("All fields are required and cannot be empty.");
                return;
            }

            using (var conn = dbConnector.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES (@first, @last, @mail, @phone, @address)", conn);

                // Handle null values for Address as it's optional
                cmd.Parameters.AddWithValue("@first", customer.FirstName);
                cmd.Parameters.AddWithValue("@last", customer.LastName);
                cmd.Parameters.AddWithValue("@mail", customer.Email);
                cmd.Parameters.AddWithValue("@phone", customer.Phone);
                cmd.Parameters.AddWithValue("@address", customer.Address ?? (object)DBNull.Value); // Allow null for Address

                cmd.ExecuteNonQuery();
            }
        }



        public void UpdateCustomer(Customers customer)
        {
            using var conn = dbConnector.GetConnection();
            conn.Open();
            SqlCommand cmd = new("UPDATE Customers SET FirstName=@first, LastName=@last, Email=@mail, Phone=@phone, Address=@address WHERE CustomerID=@id", conn);
            cmd.Parameters.AddWithValue("@id", customer.CustomerID);
            cmd.Parameters.AddWithValue("@first", customer.FirstName);
            cmd.Parameters.AddWithValue("@last", customer.LastName);
            cmd.Parameters.AddWithValue("@mail", customer.Email);
            cmd.Parameters.AddWithValue("@phone", customer.Phone);
            cmd.Parameters.AddWithValue("@address", customer.Address);
            cmd.ExecuteNonQuery();
        }

        public void DeleteCustomer(int customerId)
        {
            using var conn = dbConnector.GetConnection();
            conn.Open();
            SqlCommand cmd = new("DELETE FROM Customers WHERE CustomerID=@id", conn);
            cmd.Parameters.AddWithValue("@id", customerId);
            cmd.ExecuteNonQuery();
        }

        public List<Customers> GetAllCustomers()
        {
            var customers = new List<Customers>();
            using var conn = dbConnector.GetConnection();
            conn.Open();
            SqlCommand cmd = new("SELECT * FROM Customers", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new Customers
                {
                    CustomerID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4),
                    Address = reader.GetString(5)
                });
            }
            return customers;
        }
        public bool UpdateCustomerInfo(int customerId, string newEmail, string newPhone)
        {
            try
            {
                using (SqlConnection conn = dbConnector.GetConnection())
                {
                    string query = "UPDATE Customers SET Email = @Email, Phone = @Phone WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", newEmail);
                        cmd.Parameters.AddWithValue("@Phone", newPhone);
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating customer info: {ex.Message}");
                return false;
            }
        }
    }
}