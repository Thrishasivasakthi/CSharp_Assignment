using System;

namespace TicketBookingSystem.entity
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public Venue Venue { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; }

        public Event() { }

        public Event(int eventId, string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue,
                     int totalSeats, int availableSeats, decimal ticketPrice, string eventType)
        {
            EventId = eventId;
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            Venue = venue;
            TotalSeats = totalSeats;
            AvailableSeats = availableSeats;
            TicketPrice = ticketPrice;
            EventType = eventType;
        }

        public void DisplayEventDetails()
        {
            string venueName = Venue != null ? Venue.VenueName : "N/A";

            Console.WriteLine($"Event_ID: {EventId}, Event_Name: {EventName}, Event_Type: {EventType}, " +
                              $"Event_Date: {EventDate.ToShortDateString()}, Event_Time: {EventTime}, " +
                              $"Venue_Name: {venueName}, Total_Seats: {TotalSeats}, Available_Seats: {AvailableSeats}, " +
                              $"Ticket_Price: {TicketPrice}");
        }
    }
}
