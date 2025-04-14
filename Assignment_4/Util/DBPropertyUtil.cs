using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace CourierManagementSystem.Util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString(string fileName)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();

            foreach (string line in File.ReadAllLines(fileName))
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                {
                    var tokens = line.Split('=');
                    if (tokens.Length == 2)
                    {
                        properties[tokens[0].Trim()] = tokens[1].Trim();
                    }
                }
            }

            return $"Server={properties["server"]};Database={properties["database"]};User Id={properties["user"]};Password={properties["password"]};";
        }
    }
}
