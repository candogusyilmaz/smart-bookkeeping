using System.Configuration;

namespace StockManagementSystem.Library
{
    public static class DataAccessHelpers
    {
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
