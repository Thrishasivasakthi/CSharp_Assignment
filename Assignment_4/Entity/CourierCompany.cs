using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CourierManagementSystem.Entity
{
    public class CourierCompany
    {
        public string CompanyName { get; set; }
        public Courier[] CourierDetails { get; set; }
        public Employee[] EmployeeDetails { get; set; }
        public Location[] LocationDetails { get; set; }

        public CourierCompany() { }

        public CourierCompany(string companyName, Courier[] courierDetails, Employee[] employeeDetails, Location[] locationDetails)
        {
            CompanyName = companyName;
            CourierDetails = courierDetails;
            EmployeeDetails = employeeDetails;
            LocationDetails = locationDetails;
        }

        public override string ToString()
        {
            return $"Company: {CompanyName}, Couriers: {CourierDetails.Length}, Employees: {EmployeeDetails.Length}, Locations: {LocationDetails.Length}";
        }
    }
}

