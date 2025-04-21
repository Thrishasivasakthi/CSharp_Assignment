using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.exception
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message) { }
    }
}
