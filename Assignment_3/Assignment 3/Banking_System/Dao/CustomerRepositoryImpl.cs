using Banking_System.Entity;
using Banking_System.Util;
using Microsoft.Data.SqlClient;

namespace Banking_System.DAO
{
    public class CustomerRepositoryImpl : ICustomerRepository
    {
        public bool IsCustomerExists(int customerId)
        {
            using (SqlConnection connection = DBConnUtil.GetDBConnection())
            {
                string query = "SELECT COUNT(*) FROM Customers WHERE customer_id = @customerId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection connection = DBConnUtil.GetDBConnection())
            {
                string query = "INSERT INTO Customers (first_name, last_name, email, phone_number, address) VALUES (@firstName, @lastName, @email, @phoneNumber, @address)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", customer.Address);

                cmd.ExecuteNonQuery();
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            using (SqlConnection connection = DBConnUtil.GetDBConnection())
            {
                string query = "SELECT * FROM Customers WHERE customer_id = @customerId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Customer
                    {
                        CustomerId = (int)reader["customer_id"],
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Email = reader["email"].ToString(),
                        PhoneNumber = reader["phone_number"].ToString(),
                        Address = reader["address"].ToString()
                    };
                }
            }
            return null; 
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = DBConnUtil.GetDBConnection())
            {
                string query = "UPDATE Customers SET first_name = @firstName, last_name = @lastName, email = @email, phone_number = @phoneNumber, address = @address WHERE customer_id = @customerId";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", customer.Address);

                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection connection = DBConnUtil.GetDBConnection())
            {
                string query = "DELETE FROM Customers WHERE customer_id = @customerId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
