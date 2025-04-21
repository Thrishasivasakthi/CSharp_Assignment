using System.Configuration;

namespace assignment_2.util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
