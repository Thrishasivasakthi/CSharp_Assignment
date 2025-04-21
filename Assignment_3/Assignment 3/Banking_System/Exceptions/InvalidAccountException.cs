using System;

namespace Banking_System.ExceptionHandling
{
    public class InvalidAccountException : Exception
    {
        public InvalidAccountException(string message) : base(message) { }
    }
}
