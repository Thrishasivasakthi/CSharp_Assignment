using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CourierManagementSystem.Exception
{
    public class TrackingNumberNotFoundException : ApplicationException
    {
        public TrackingNumberNotFoundException() : base("Tracking number not found.")
        {
        }

        public TrackingNumberNotFoundException(string message) : base(message)
        {
        }
    }
}

