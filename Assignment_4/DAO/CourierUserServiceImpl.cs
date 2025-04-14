using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierManagementSystem.Entity;
using CourierManagementSystem.Exception;

namespace CourierManagementSystem.DAO
{
    public class CourierUserServiceImpl : ICourierUserService
    {
        protected CourierCompany companyObj;

        public CourierUserServiceImpl(CourierCompany companyObj)
        {
            this.companyObj = companyObj;
        }

        public string PlaceOrder(Courier courierObj)
        {
            for (int i = 0; i < companyObj.CourierDetails.Length; i++)
            {
                if (companyObj.CourierDetails[i] == null)
                {
                    companyObj.CourierDetails[i] = courierObj;
                    return courierObj.TrackingNumber;
                }
            }
            return null;
        }

        public string GetOrderStatus(string trackingNumber)
        {
            foreach (var courier in companyObj.CourierDetails)
            {
                if (courier != null && courier.TrackingNumber.Equals(trackingNumber))
                {
                    return courier.Status;
                }
            }
            throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found.");
        }

        public bool CancelOrder(string trackingNumber)
        {
            for (int i = 0; i < companyObj.CourierDetails.Length; i++)
            {
                if (companyObj.CourierDetails[i] != null && companyObj.CourierDetails[i].TrackingNumber.Equals(trackingNumber))
                {
                    companyObj.CourierDetails[i].Status = "Cancelled";
                    return true;
                }
            }
            return false;
        }

        public List<Courier> GetAssignedOrder(int courierStaffId)
        {
            List<Courier> result = new List<Courier>();
            foreach (var courier in companyObj.CourierDetails)
            {
                if (courier != null && courier.UserId == courierStaffId)
                {
                    result.Add(courier);
                }
            }
            return result;
        }
    }
}

