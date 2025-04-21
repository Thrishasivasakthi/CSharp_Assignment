using System;

namespace TicketBookingSystem.exception
{
    public class InvalidBookingIDException : Exception
    {
        public InvalidBookingIDException(string message) : base(message)
        {
        }
    }
}

