using Banking_System.Entity;

namespace Banking_System.Service
{
    public interface ICustomerServiceProvider
    {
        bool IsCustomerExists(int customerId);
        Customer GetCustomerDetails(int customerId);
    }
}
