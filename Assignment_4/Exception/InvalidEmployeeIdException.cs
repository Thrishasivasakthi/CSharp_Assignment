using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourierManagementSystem.Exception
{
    public class InvalidEmployeeIdException : ApplicationException
    {
        public InvalidEmployeeIdException() : base("Invalid employee ID.")
        {
        }

        public InvalidEmployeeIdException(string message) : base(message)
        {
        }
    }
}

