using CourierManagementSystem.DAO;
using CourierManagementSystem.Entity;
using CourierManagementSystem.Exception;
using System;
namespace CourierManagementSystem.Dao
{
    public class CourierAdminServiceCollectionImpl : CourierCompanyCollection, ICourierAdminService
    {
        // Constructor calling base constructor
        public CourierAdminServiceCollectionImpl(CourierCompanyCollection companyObj)
            : base(companyObj)
        {
            CompanyObj = companyObj ?? throw new ArgumentNullException(nameof(companyObj), "Company object cannot be null.");
        }

        // Changed type of CompanyObj to CourierCompanyCollection to fix CS1061
        public CourierCompanyCollection CompanyObj { get; private set; }

        public int AddCourierStaff(Employee obj)
        {
            if (obj == null || obj.EmployeeID <= 0)
            {
                throw new InvalidEmployeeIdException("Invalid employee details provided.");
            }

            CompanyObj.EmployeeDetails.Add(obj); // This now works because CompanyObj is of type CourierCompanyCollection
            return obj.EmployeeID;
        }
    }
}
