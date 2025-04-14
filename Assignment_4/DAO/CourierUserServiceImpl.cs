using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierManagementSystem.Entity;
using CourierManagementSystem.Exception;
using CourierManagementSystem.DAO;

namespace CourierManagementSystem.DAO
{
    public class CourierUserServiceImpl : ICourierUserService
    {
        protected CourierCompanyCollection companyObj;
        private readonly CourierServiceDb _dbService;

        public CourierUserServiceImpl(CourierCompanyCollection companyObj)
        {
            this.companyObj = companyObj ?? throw new ArgumentNullException(nameof(companyObj));
            _dbService = new CourierServiceDb();
        }

        public string PlaceOrder(Courier courierObj)
        {
            for (int i = 0; i < companyObj.CourierDetails.Count; i++)
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
            try
            {
                Console.WriteLine($"Attempting to cancel order with TrackingNumber: {trackingNumber}");
                bool dbSuccess = _dbService.CancelOrderInDb(trackingNumber);
                Console.WriteLine($"Database update success: {dbSuccess}");
                if (dbSuccess)
                {
                    
                    for (int i = 0; i < companyObj.CourierDetails.Count; i++)
                    {
                        if (companyObj.CourierDetails[i] != null && companyObj.CourierDetails[i].TrackingNumber.Equals(trackingNumber))
                        {
                            companyObj.CourierDetails[i].Status = "Cancelled";
                            Console.WriteLine($"Updated in-memory status to Cancelled");
                            break;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error in CancelOrder: {ex.Message} | StackTrace: {ex.StackTrace}");
                return false;
            }
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

