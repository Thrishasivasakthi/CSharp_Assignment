using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourierManagementSystem.Entity;
using CourierManagementSystem.Exception;

namespace CourierManagementSystem.DAO
{
    public class CourierAdminServiceImpl : CourierUserServiceImpl, ICourierAdminService
    {
        public CourierAdminServiceImpl(CourierCompanyCollection companyObj) : base(companyObj) { }

        public int AddCourierStaff(Employee obj)
        {
            for (int i = 0; i < companyObj.EmployeeDetails.Count; i++)
            {
                if (companyObj.EmployeeDetails[i] == null)
                {
                    companyObj.EmployeeDetails[i] = obj;
                    return obj.EmployeeID;
                }
            }
            throw new InvalidEmployeeIdException("Employee storage full. Cannot add more staff.");
        }
    }
}

