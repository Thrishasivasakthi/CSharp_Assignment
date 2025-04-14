using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CourierManagementSystem.Entity
{
    public class Courier
    {
        private static int trackingSeed = 1000;

        public long CourierID { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get;  set; }
        public DateTime? DeliveryDate { get; set; }
        public int? UserId { get; set; }
      

        public Courier()
        {
            TrackingNumber = $"TRK{trackingSeed++}";
        }

        public Courier(int courierID, string senderName, string senderAddress, string receiverName, string receiverAddress, decimal weight, string status, DateTime deliveryDate, int userId)
        {
            CourierID = courierID;
            SenderName = senderName;
            SenderAddress = senderAddress;
            ReceiverName = receiverName;
            ReceiverAddress = receiverAddress;
            Weight = weight;
            Status = status;
            DeliveryDate = deliveryDate;
            UserId = userId;
            TrackingNumber = $"TRK{trackingSeed++}";
        }

        public override string ToString()
        {
            return $"CourierID: {CourierID}, Sender: {SenderName}, Receiver: {ReceiverName}, Status: {Status}, Tracking: {TrackingNumber}";
        }
    }
}

