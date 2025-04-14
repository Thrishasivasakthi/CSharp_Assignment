
using CourierManagementSystem.Entity;
using CourierManagementSystem.Exception;
using System;
namespace CourierManagementSystem.DAO
{
    public class CourierAdminServiceCollectionImpl : CourierUserServiceImpl, ICourierAdminService
    {
        private readonly CourierServiceDb _dbService; 

   
        public CourierAdminServiceCollectionImpl(CourierCompanyCollection companyObj)
            : base(companyObj)
        {
            CompanyObj = companyObj ?? throw new ArgumentNullException(nameof(companyObj), "Company object cannot be null.");
            _dbService = new CourierServiceDb();
        }

        
        public CourierCompanyCollection CompanyObj { get; private set; }

        public int AddCourierStaff(Employee obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.EmployeeName) || obj.EmployeeID <= 0 || string.IsNullOrWhiteSpace(obj.Email) || string.IsNullOrWhiteSpace(obj.Role))
            {
                throw new InvalidEmployeeIdException("Invalid employee details provided.");
            }

            CompanyObj.EmployeeDetails.Add(obj); 
            try
            {
                _dbService.InsertEmployee(obj); 
                return obj.EmployeeID; 
            }
            catch (System.Exception ex) 
            {
                Console.WriteLine($"Error adding employee to database: {ex.Message}");
                throw;
            }

        }
    }
}
