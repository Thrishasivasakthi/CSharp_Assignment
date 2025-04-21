using System;
using TicketBookingSystem.entity;

namespace TicketBookingSystem.dao
{
    public interface IBookingSystemServiceProvider
    {
        Booking BookTickets(int bookingId, Customer customer, Event eventObj, int numTickets);
        void CancelBooking(int bookingId);
        decimal CalculateBookingCost(Event eventObj, int numTickets);
        Booking GetBookingDetails(int bookingId);
    }
}


