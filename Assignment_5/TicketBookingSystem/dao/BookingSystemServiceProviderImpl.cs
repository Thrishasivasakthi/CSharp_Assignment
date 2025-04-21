using System;
using System.Collections.Generic;
using TicketBookingSystem.entity;
using TicketBookingSystem.exception;

namespace TicketBookingSystem.dao
{
    public class BookingSystemServiceProviderImpl : EventServiceProviderImpl, IBookingSystemServiceProvider
    {
        private List<Booking> bookings = new List<Booking>();

        public decimal CalculateBookingCost(Event eventObj, int numTickets)
        {
            return eventObj.TicketPrice * numTickets;
        }

        public Booking BookTickets(int bookingId, Customer customer, Event eventObj, int numTickets)
        {
            if (eventObj.AvailableSeats < numTickets)
            {
                throw new Exception("Not enough available seats.");
            }

            decimal totalCost = CalculateBookingCost(eventObj, numTickets);
            eventObj.AvailableSeats -= numTickets;

            Booking booking = new Booking(bookingId, customer, eventObj, numTickets, totalCost, DateTime.Now);
            bookings.Add(booking);

            Console.WriteLine("Booking successful!");
            return booking;
        }

        public void CancelBooking(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new InvalidBookingIDException($"Booking with ID {bookingId} not found.");
            }

            booking.Event.AvailableSeats += booking.NumTickets;
            bookings.Remove(booking);

            Console.WriteLine("Booking cancelled successfully.");
        }

        public Booking GetBookingDetails(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new InvalidBookingIDException($"Booking with ID {bookingId} not found.");
            }
            return booking;
        }
    }
}
