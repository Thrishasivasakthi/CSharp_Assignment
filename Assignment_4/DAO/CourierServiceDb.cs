using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace CourierManagementSystem.DAO
{
    public class CourierServiceDb
    {
        private static SqlConnection connection;

        public CourierServiceDb(string propFile)
        {
            connection = Util.DBConnUtil.GetConnection(propFile);
        }

        public void InsertCourier(CourierManagementSystem.Entity.Courier courier)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Courier (CourierID, SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, TrackingNumber, DeliveryDate) " +
                               "VALUES (@CourierID, @SenderName, @SenderAddress, @ReceiverName, @ReceiverAddress, @Weight, @Status, @TrackingNumber, @DeliveryDate)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CourierID", courier.CourierID);
                cmd.Parameters.AddWithValue("@SenderName", courier.SenderName);
                cmd.Parameters.AddWithValue("@SenderAddress", courier.SenderAddress);
                cmd.Parameters.AddWithValue("@ReceiverName", courier.ReceiverName);
                cmd.Parameters.AddWithValue("@ReceiverAddress", courier.ReceiverAddress);
                cmd.Parameters.AddWithValue("@Weight", courier.Weight);
                cmd.Parameters.AddWithValue("@Status", courier.Status);
                cmd.Parameters.AddWithValue("@TrackingNumber", courier.TrackingNumber);
                cmd.Parameters.AddWithValue("@DeliveryDate", courier.DeliveryDate);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting courier: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        // Similarly add: UpdateCourierStatus, GetDeliveryHistory etc.
    }
}

