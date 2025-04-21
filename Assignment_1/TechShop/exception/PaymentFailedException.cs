using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.exception
{
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException(string message) : base(message) { }
    }
}
