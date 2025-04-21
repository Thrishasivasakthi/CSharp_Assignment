using Banking_System.Entity;
using Microsoft.Data.SqlClient;

namespace Banking_System.DAO
{
    public class BankRepositoryImpl : IBankRepository
    {
        internal readonly string _connectionString;

        public BankRepositoryImpl(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddAccount(Account account)
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                string customerIdd = Console.ReadLine();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string queryCheckCustomer = "SELECT COUNT(*) FROM Customers WHERE customer_id = @CustomerId";
                    SqlCommand cmdCheckCustomer = new SqlCommand(queryCheckCustomer, connection);
                    cmdCheckCustomer.Parameters.AddWithValue("@CustomerId", customerIdd);

                    connection.Open();
                    int count = (int)cmdCheckCustomer.ExecuteScalar();

                    if (count > 0)
                    {
                        string queryInsertAccount = "INSERT INTO Accounts (customer_id, account_type, balance) VALUES (@customerId, @accountType, @balance)";
                        SqlCommand cmdInsertAccount = new SqlCommand(queryInsertAccount, connection);
                        cmdInsertAccount.Parameters.AddWithValue("@customerId", customerIdd);
                        cmdInsertAccount.Parameters.AddWithValue("@accountType", account.AccountType);
                        cmdInsertAccount.Parameters.AddWithValue("@balance", account.Balance);

                        cmdInsertAccount.ExecuteNonQuery();

                        Console.WriteLine($"Account created successfully with balance {account.Balance}");
                    }
                    else
                    {
                        Console.WriteLine("Customer ID not found. Please enter a valid Customer ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public Account GetAccountById(long accountId)
        {
            Account account = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Accounts WHERE account_id = @accountId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@accountId", accountId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        account = new Account
                        {
                            AccountId = Convert.ToInt64(reader["account_id"]),
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            AccountType = reader["account_type"].ToString(),
                            Balance = Convert.ToDecimal(reader["balance"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return account;
        }

        public void UpdateAccount(Account account)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Accounts SET customer_id = @customerId, account_type = @accountType, balance = @balance WHERE account_id = @accountId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@accountId", account.AccountId);
                    cmd.Parameters.AddWithValue("@customerId", account.CustomerId);
                    cmd.Parameters.AddWithValue("@accountType", account.AccountType);
                    cmd.Parameters.AddWithValue("@balance", account.Balance);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Account with ID {account.AccountId} updated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteAccount(long accountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM Accounts WHERE account_id = @accountId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@accountId", accountId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Account with ID {accountId} deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Transactions (account_id, transaction_type, amount, transaction_date) VALUES (@accountId, @transactionType, @amount, @transactionDate)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@accountId", transaction.AccountId);
                    cmd.Parameters.AddWithValue("@transactionType", transaction.TransactionType);
                    cmd.Parameters.AddWithValue("@amount", transaction.Amount);
                    cmd.Parameters.AddWithValue("@transactionDate", transaction.TransactionDate);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Transaction added: {transaction.TransactionType} of {transaction.Amount}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public List<Transaction> GetTransactionsByAccountId(long accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Transactions WHERE account_id = @accountId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@accountId", accountId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction
                        {
                            TransactionId = Convert.ToInt32(reader["transaction_id"]),
                            AccountId = Convert.ToInt64(reader["account_id"]),
                            TransactionType = reader["transaction_type"].ToString(),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            TransactionDate = Convert.ToDateTime(reader["transaction_date"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return transactions;
        }
    }
}
