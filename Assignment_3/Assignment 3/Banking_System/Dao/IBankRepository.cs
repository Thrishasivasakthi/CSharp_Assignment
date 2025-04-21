using Banking_System.Entity;

namespace Banking_System.DAO
{
    public interface IBankRepository
    {
        void AddAccount(Account account);
        Account GetAccountById(long accountId);
        void UpdateAccount(Account account);
        void DeleteAccount(long accountId);
        void AddTransaction(Transaction transaction);
        List<Transaction> GetTransactionsByAccountId(long accountId);
    }
}
