namespace Banking_System.Util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString()
        {
            return "Server=LOCALHOST;Database=HMBank;Integrated Security=True;TrustServerCertificate=True;";
        }

    }
}
