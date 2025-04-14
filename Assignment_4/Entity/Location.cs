using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Entity
{
    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }

        public Location() { }

        public Location(int locationID, string locationName, string address)
        {
            LocationID = locationID;
            LocationName = locationName;
            Address = address;
        }

        public override string ToString()
        {
            return $"LocationID: {LocationID}, Name: {LocationName}, Address: {Address}";
        }
    }
}

