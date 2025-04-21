using System;

namespace Banking_System.ExceptionHandling
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }
}
