using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.exception
{
    public class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException(string message) : base(message) { }
    }
}
