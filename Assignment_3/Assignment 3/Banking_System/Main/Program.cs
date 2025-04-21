using System;
using Banking_System.Entity;
using Banking_System.DAO;

namespace Banking_System.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=JARVIS-LAPTOP;Database=HMBank;Integrated Security=True;TrustServerCertificate=True;";
            BankRepositoryImpl bankRepo = new BankRepositoryImpl(connectionString);
            Bank bank = new Bank(bankRepo);
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("- - - - - - Welcome to HMBank! - - - - - -");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        bank.CreateAccount();
                        break;
                    case 2:
                        bank.Deposit();
                        break;
                    case 3:
                        bank.Withdraw();
                        break;
                    case 4:
                        bank.GetBalance();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }

    public class Bank
    {
        private BankRepositoryImpl _bankRepo;

        public Bank(BankRepositoryImpl bankRepo)
        {
            _bankRepo = bankRepo;
        }

        public void CreateAccount()
        {
            Console.WriteLine("Enter Customer Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Initial Deposit Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Account newAccount = new Account
            {
                AccountType = "Savings",
                Balance = amount,
                AccountHolder = new Customer { FirstName = name }
            };

            _bankRepo.AddAccount(newAccount);
            Console.WriteLine($"Account created successfully with balance {amount}.");
        }

        public void Deposit()
        {
            Console.WriteLine("Enter Account Number: ");
            long accountNumber = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Deposit Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Account account = _bankRepo.GetAccountById(accountNumber);
            if (account != null)
            {
                account.Deposit(amount);
                _bankRepo.UpdateAccount(account);
                Console.WriteLine($"Deposited {amount}. New balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void Withdraw()
        {
            Console.WriteLine("Enter Account Number: ");
            long accountNumber = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Withdrawal Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Account account = _bankRepo.GetAccountById(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount);
                _bankRepo.UpdateAccount(account);
                Console.WriteLine($"Withdrew {amount}. New balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void GetBalance()
        {
            Console.WriteLine("Enter Account Number: ");
            long accountNumber = long.Parse(Console.ReadLine());

            Account account = _bankRepo.GetAccountById(accountNumber);
            if (account != null)
            {
                Console.WriteLine($"Current Balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }
}
