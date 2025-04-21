using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Booking
    {
        public int BookingId { get; set; }
        public Customer Customer { get; set; }
        public Event Event { get; set; }
        public int NumTickets { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking() { }

        public Booking(int bookingId, Customer customer, Event bookedEvent, int numTickets, decimal totalCost, DateTime bookingDate)
        {
            BookingId = bookingId;
            Customer = customer;
            Event = bookedEvent;
            NumTickets = numTickets;
            TotalCost = totalCost;
            BookingDate = bookingDate;
        }

        public void DisplayBookingDetails()
        {
            Console.WriteLine($"Booking ID: {BookingId}, Customer: {Customer.CustomerName}, Event: {Event.EventName}, " +
                              $"Tickets: {NumTickets}, Total Cost: {TotalCost}, Booking Date: {BookingDate}");
        }
    }
}
