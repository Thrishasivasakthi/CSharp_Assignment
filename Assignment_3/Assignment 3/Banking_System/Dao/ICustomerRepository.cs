using Banking_System.Entity;

namespace Banking_System.DAO
{
    public interface ICustomerRepository
    {
        // Method to check if a customer exists by customer_id
        bool IsCustomerExists(int customerId);

        // Method to add a new customer
        void AddCustomer(Customer customer);

        // Method to retrieve customer details by customer_id
        Customer GetCustomerById(int customerId);

        // Method to update a customer's details
        void UpdateCustomer(Customer customer);

        // Method to delete a customer by customer_id
        void DeleteCustomer(int customerId);
    }
}
