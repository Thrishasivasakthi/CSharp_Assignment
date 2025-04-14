using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierManagementSystem.Entity;

namespace CourierManagementSystem.DAO
{
    public interface ICourierAdminService
    {
        int AddCourierStaff(Employee obj);
    }
}

