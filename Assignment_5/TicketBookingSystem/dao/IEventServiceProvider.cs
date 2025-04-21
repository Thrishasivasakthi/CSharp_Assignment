using System.Collections.Generic;
using TicketBookingSystem.entity;

namespace TicketBookingSystem.dao
{
    public interface IEventServiceProvider
    {
        void DisplayEvents();
        void CreateEvent(Event eventObj);
        List<Event> GetEventDetails();
        int GetAvailableNoOfTickets(int eventId);
    }
}
