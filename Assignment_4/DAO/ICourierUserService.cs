using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierManagementSystem.Entity;

namespace CourierManagementSystem.DAO
{
    public interface ICourierUserService
    {
        string PlaceOrder(Courier courierObj);

        string GetOrderStatus(string trackingNumber);

        bool CancelOrder(string trackingNumber);

        List<Courier> GetAssignedOrder(int courierStaffId);
    }
}
