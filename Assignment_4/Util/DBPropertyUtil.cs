using System;
using System.Collections.Generic;
using System.IO;

namespace CourierManagementSystem.Util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString(string fileName)
        {
            var properties = new Dictionary<string, string>();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                {
                    var tokens = line.Split('=');
                    if (tokens.Length == 2)
                    {
                        properties[tokens[0].Trim().ToLower()] = tokens[1].Trim();
                    }
                }
            }

            return $"Server={properties["server"] ?? properties["Server"]};" +
                   $"Database={properties["database"] ?? properties["Database"]};" +
                   (properties.ContainsKey("trusted_connection") || properties.ContainsKey("Trusted_Connection")
                       ? "Trusted_Connection=True;"
                       : $"User Id={properties["user"] ?? properties["User Id"]};Password={properties["password"] ?? properties["Password"]};");
        }
    }
}