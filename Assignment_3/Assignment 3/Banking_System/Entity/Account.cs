using System;

namespace Banking_System.Entity
{
    public class Account
    {
        public long AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public Customer AccountHolder { get; set; }
        public long AccountId { get; internal set; }
        public int CustomerId { get; internal set; }

        public Account() { }

        public Account(long accountNumber, string accountType, decimal balance, Customer accountHolder)
        {
            AccountNumber = accountNumber;
            AccountType = accountType;
            Balance = balance;
            AccountHolder = accountHolder;
        }
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Deposited: {amount}. New Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }
        public void Withdraw(decimal amount)
        {
            if (amount > 0 && Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}. New Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance or invalid withdrawal amount.");
            }
        }
        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Account Type: {AccountType}");
            Console.WriteLine($"Account Balance: {Balance}");
            Console.WriteLine($"Account Holder: {AccountHolder.FirstName} {AccountHolder.LastName}");
        }
    }
}
