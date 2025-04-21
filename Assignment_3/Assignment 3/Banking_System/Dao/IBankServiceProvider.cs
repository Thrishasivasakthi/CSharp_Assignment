using Banking_System.Entity;

namespace Banking_System.Service
{
    public interface IBankServiceProvider : ICustomerServiceProvider
    {
        void CreateAccount(Account account);
        void Deposit(long accountNumber, decimal amount);
        void Withdraw(long accountNumber, decimal amount);
        decimal GetBalance(long accountNumber);
        void Transfer(long fromAccount, long toAccount, decimal amount);
    }
}
