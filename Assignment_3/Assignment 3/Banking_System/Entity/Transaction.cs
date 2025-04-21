namespace Banking_System.Entity
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public long AccountId { get; set; }  
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transaction(int transactionId, long accountId, string transactionType, decimal amount, DateTime transactionDate)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            TransactionType = transactionType;
            Amount = amount;
            TransactionDate = transactionDate;
        }

        public Transaction() { }
    }
}
