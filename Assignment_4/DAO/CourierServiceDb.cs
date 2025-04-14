using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CourierManagementSystem.Entity;
using CourierManagementSystem.Exception;
using CourierManagementSystem.Util;

namespace CourierManagementSystem.DAO
{
    public class CourierServiceDb
    {
        private readonly string _propFile; 

        public CourierServiceDb(string propFile = "db.properties")
        {
            _propFile = propFile ?? "db.properties"; 
            Console.WriteLine($"Using properties file: {_propFile}");
        }

        public void InsertCourier(Courier courier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(courier.SenderName) || string.IsNullOrWhiteSpace(courier.SenderAddress) ||
                    string.IsNullOrWhiteSpace(courier.ReceiverName) || string.IsNullOrWhiteSpace(courier.ReceiverAddress) ||
                    courier.Weight <= 0 || string.IsNullOrWhiteSpace(courier.Status) || string.IsNullOrWhiteSpace(courier.TrackingNumber))
                {
                    throw new ArgumentException("SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, and TrackingNumber are required and cannot be empty or invalid.");
                }
                using (var connection = DBConnUtil.GetConnection(_propFile))
                {
                    //Console.WriteLine($"Opening connection for InsertCourier");
                    connection.Open();
                    //Console.WriteLine($"Connection opened, state: {connection.State}");
                    string query = "INSERT INTO Couriers (SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, TrackingNumber, DeliveryDate, UserID) " +
                                   "VALUES (@SenderName, @SenderAddress, @ReceiverName, @ReceiverAddress, @Weight, @Status, @TrackingNumber, @DeliveryDate, @UserID)";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SenderName", (object)courier.SenderName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SenderAddress", (object)courier.SenderAddress ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ReceiverName", (object)courier.ReceiverName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ReceiverAddress", (object)courier.ReceiverAddress ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Weight", courier.Weight);
                        cmd.Parameters.AddWithValue("@Status", (object)courier.Status ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrackingNumber", (object)courier.TrackingNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DeliveryDate", courier.DeliveryDate.HasValue ? (object)courier.DeliveryDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserID", courier.UserId);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted courier with TrackingNumber: {courier.TrackingNumber}");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error inserting courier: {ex.Message} | Number: {ex.Number} | State: {ex.State}");
                throw new InvalidOperationException("Failed to insert courier order.", ex);
            }
            catch (SystemException ex)
            {
                Console.WriteLine($"Unexpected error inserting courier: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void GenerateReport()
        {
            try
            {
                Console.WriteLine("Generating report...");
                using (var connection = DBConnUtil.GetConnection(_propFile))
                {
                    connection.Open();
                    //Console.WriteLine($"Connection opened, state: {connection.State}");
                    using (var cmd = new SqlCommand("SELECT TrackingNumber, Status FROM Couriers", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No records found in Couriers table.");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader["TrackingNumber"]}: {reader["Status"]}");
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Report generation complete.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message} | Number: {ex.Number} | State: {ex.State}");
            }
            catch (SystemException ex)
            {
                Console.WriteLine($"Unexpected error generating report: {ex.Message} | StackTrace: {ex.StackTrace}");
            }
        }

        public void InsertEmployee(Employee employee)
        {
            try
            {
                using (var connection = DBConnUtil.GetConnection(_propFile))
                {
                    //Console.WriteLine($"Opening connection for InsertEmployee");
                    connection.Open();
                    //Console.WriteLine($"Connection opened, state: {connection.State}");
                    string query = "INSERT INTO Employees (Name, Email, ContactNumber, Role, Salary) " +
                                   "OUTPUT INSERTED.EmployeeID VALUES (@Name, @Email, @ContactNumber, @Role, @Salary)";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", employee.EmployeeName);
                        cmd.Parameters.AddWithValue("@Email", employee.Email);
                        cmd.Parameters.AddWithValue("@ContactNumber", (object)employee.ContactNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Role", employee.Role);
                        cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                        employee.EmployeeID = (int)cmd.ExecuteScalar();
                        Console.WriteLine($"Inserted employee with ID: {employee.EmployeeID}");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error adding employee: {ex.Message}");
                throw new InvalidOperationException("Failed to insert employee.", ex);
            }
        }

        public List<Courier> GetDeliveryHistory(string trackingNumber)
        {
            var deliveryHistory = new List<Courier>();

            try
            {
                using (var connection = DBConnUtil.GetConnection(_propFile))
                {
                    Console.WriteLine($"Opening connection for GetDeliveryHistory with TrackingNumber: {trackingNumber}");
                    connection.Open();
                    Console.WriteLine($"Connection opened, state: {connection.State}");
                    string query = "SELECT * FROM Couriers WHERE TrackingNumber = @TrackingNumber";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var courier = new Courier
                                {
                                    CourierID = reader.GetInt32(0),
                                    SenderName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    SenderAddress = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    ReceiverName = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    ReceiverAddress = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Weight = reader.GetDecimal(5),
                                    Status = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    TrackingNumber = reader.GetString(7),
                                    DeliveryDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                                    UserId = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9)
                                };
                                deliveryHistory.Add(courier);
                            }
                        }
                    }
                }
                if (deliveryHistory.Count == 0)
                    throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found.");
                Console.WriteLine($"Retrieved {deliveryHistory.Count} record(s) for {trackingNumber}");
                return deliveryHistory;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error retrieving delivery history: {ex.Message}");
                throw new InvalidOperationException("Failed to retrieve delivery history.", ex);
            }
        }

        public bool CancelOrderInDb(string trackingNumber)
        {
            try
            {
                //Console.WriteLine($"Opening connection for CancelOrderInDb with TrackingNumber: {trackingNumber}");
                using (var connection = DBConnUtil.GetConnection(_propFile))
                {
                    connection.Open();
                    //Console.WriteLine($"Connection opened, state: {connection.State}");

                    string checkQuery = "SELECT Status FROM Couriers WHERE TrackingNumber = @TrackingNumber";
                    using (var checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);
                        object result = checkCmd.ExecuteScalar();
                        string currentStatus = result != null ? result.ToString() : "Not Found";
                        Console.WriteLine($"Current status for {trackingNumber}: {currentStatus}");
                    }

                    string query = "UPDATE Couriers SET Status = 'Cancelled' WHERE TrackingNumber = @TrackingNumber AND Status = 'Processing'";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected by update: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error cancelling order: {ex.Message} | Number: {ex.Number} | State: {ex.State}");
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Unexpected error in CancelOrderInDb: {ex.Message} | StackTrace: {ex.StackTrace}");
                return false;
            }
        }
    }
}