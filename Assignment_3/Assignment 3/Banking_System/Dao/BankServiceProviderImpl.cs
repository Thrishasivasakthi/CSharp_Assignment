using Banking_System.DAO;
using Banking_System.Entity;

namespace Banking_System.Service
{
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        public BankServiceProviderImpl(IBankRepository bankRepository) : base(bankRepository) { }

        public void CreateAccount(Account account)
        {
            _bankRepository.AddAccount(account);
        }

        public void Deposit(long accountNumber, decimal amount)
        {
            var account = _bankRepository.GetAccountById(accountNumber);
            if (account != null)
            {
                account.Balance += amount;
                _bankRepository.UpdateAccount(account);
            }
        }

        public void Withdraw(long accountNumber, decimal amount)
        {
            var account = _bankRepository.GetAccountById(accountNumber);
            if (account != null && account.Balance >= amount)
            {
                account.Balance -= amount;
                _bankRepository.UpdateAccount(account);
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        public decimal GetBalance(long accountNumber)
        {
            var account = _bankRepository.GetAccountById(accountNumber);
            return account?.Balance ?? 0;
        }

        public void Transfer(long fromAccount, long toAccount, decimal amount)
        {
            var fromAcc = _bankRepository.GetAccountById(fromAccount);
            var toAcc = _bankRepository.GetAccountById(toAccount);

            if (fromAcc != null && toAcc != null && fromAcc.Balance >= amount)
            {
                fromAcc.Balance -= amount;
                toAcc.Balance += amount;
                _bankRepository.UpdateAccount(fromAcc);
                _bankRepository.UpdateAccount(toAcc);
            }
            else
            {
                Console.WriteLine("Transfer failed.");
            }
        }
    }
}
