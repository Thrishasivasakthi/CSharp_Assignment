using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketBookingSystem.entity;
using TicketBookingSystem.exception;

namespace TicketBookingSystem.dao
{
    public class EventServiceProviderImpl : IEventServiceProvider
    {
        private string connectionString = "Server=localhost\\MSSQLSERVER1;Database=TicketBookingSystem;Trusted_Connection=True;";

        public void CreateEvent(Event eventObj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Event (event_id, event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) " +
                               "VALUES (@event_id, @event_name, @event_date, @event_time, @venue_id, @total_seats, @available_seats, @ticket_price, @event_type)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@event_id", eventObj.EventId);
                command.Parameters.AddWithValue("@event_name", eventObj.EventName);
                command.Parameters.AddWithValue("@event_date", eventObj.EventDate);
                command.Parameters.AddWithValue("@event_time", eventObj.EventTime);
                command.Parameters.AddWithValue("@venue_id", eventObj.Venue.VenueId);
                command.Parameters.AddWithValue("@total_seats", eventObj.TotalSeats);
                command.Parameters.AddWithValue("@available_seats", eventObj.AvailableSeats);
                command.Parameters.AddWithValue("@ticket_price", eventObj.TicketPrice);
                command.Parameters.AddWithValue("@event_type", eventObj.EventType);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Event created successfully.");
            }
        }

        public List<Event> GetEventDetails()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT e.event_id, e.event_name, e.event_date, e.event_time, e.total_seats, e.available_seats, e.ticket_price, e.event_type, " +
                               "v.venue_id, v.venue_name " +
                               "FROM Event e JOIN Venue v ON e.venue_id = v.venue_id";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Event eventObj = new Event
                        {
                            EventId = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDate = reader.GetDateTime(2),
                            EventTime = reader.GetTimeSpan(3),
                            TotalSeats = reader.GetInt32(4),
                            AvailableSeats = reader.GetInt32(5),
                            TicketPrice = reader.GetDecimal(6),
                            EventType = reader.GetString(7),
                            Venue = new Venue
                            {
                                VenueId = reader.GetInt32(8),
                                VenueName = reader.GetString(9)
                            }
                        };

                        events.Add(eventObj);
                    }
                }
            }

            return events;
        }

        public int GetAvailableNoOfTickets(int eventId)
        {
            int availableSeats = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT available_seats FROM Event WHERE event_id = @event_id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@event_id", eventId);
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    availableSeats = Convert.ToInt32(result);
                }
                else
                {
                    throw new EventNotFoundException($"Event with ID {eventId} not found.");
                }
            }

            return availableSeats;
        }

        public void DisplayEvents()
        {
            List<Event> events = GetEventDetails();
            if (events.Count == 0)
            {
                Console.WriteLine("No events available.");
                return;
            }

            foreach (var eventObj in events)
            {
                eventObj.DisplayEventDetails();
            }
        }
    }
}
