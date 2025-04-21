using System;
using System.Data.SqlClient;
using TicketBookingSystem.dao;
using TicketBookingSystem.entity;
using TicketBookingSystem.exception;

namespace TicketBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = DatabaseConnection.GetConnection();

            try
            {
                conn.Open();
                Console.WriteLine("Database Connected Successfully!");

                string query = "SELECT * FROM Booking"; 
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"BookingID: {reader["booking_id"]}");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            IEventServiceProvider eventService = new EventServiceProviderImpl();
            IBookingSystemServiceProvider bookingService = new BookingSystemServiceProviderImpl();

            while (true)
            {
                Console.WriteLine("\n======================");
                Console.WriteLine(" TICKET BOOKING SYSTEM ");
                Console.WriteLine("======================");
                Console.WriteLine("1. View Events");
                Console.WriteLine("2. Book Ticket");
                Console.WriteLine("3. Cancel Booking");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        eventService.DisplayEvents();
                        break;

                    case "2":
                        Console.Write("Enter Booking ID: ");
                        int bookingId2 = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Customer ID: ");
                        int customerId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Customer Name: ");
                        string customerName = Console.ReadLine();

                        Console.Write("Enter Customer Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Enter Customer Phone Number: ");
                        string phoneNumber = Console.ReadLine();

                        Customer customer = new Customer(customerId, customerName, email, phoneNumber);

                        Console.Write("Enter Event ID: ");
                        int eventId = Convert.ToInt32(Console.ReadLine());

                        Event eventObj = ((EventServiceProviderImpl)eventService).GetEventDetails().Find(e => e.EventId == eventId);
                        if (eventObj == null)
                        {
                            Console.WriteLine($"Event with ID {eventId} not found.");
                            break;
                        }

                        Console.Write("Enter Number of Tickets: ");
                        int numTickets = Convert.ToInt32(Console.ReadLine());

                        try
                        {
                            Booking booking = bookingService.BookTickets(bookingId2, customer, eventObj, numTickets);
                            booking.DisplayBookingDetails();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;


                    case "3":
                        Console.Write("Enter Booking ID to Cancel: ");
                        int bookingId = Convert.ToInt32(Console.ReadLine());

                        bookingService.CancelBooking(bookingId);
                        break;

                    case "4":
                        Console.WriteLine("Thank you for using the Ticket Booking System!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}

