using System;

namespace TicketBookingSystem.exception
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(string message) : base(message)
        {
        }
    }
}

