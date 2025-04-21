using Banking_System.DAO;
using Banking_System.Entity;

namespace Banking_System.Service
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        internal readonly IBankRepository _bankRepository;

        public CustomerServiceProviderImpl(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public bool IsCustomerExists(int customerId)
        {
            return true;
        }

        public Customer GetCustomerDetails(int customerId)
        {
            return new Customer(); 
        }
    }
}
