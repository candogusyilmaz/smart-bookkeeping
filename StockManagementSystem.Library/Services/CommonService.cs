using System.Collections.Generic;

namespace StockManagementSystem.Library
{
    public static class CommonService
    {
        public static IEnumerable<T> GetLastInsertedRow<T>(DbTables tableName, int count = 1)
        {
            string sql = $"select * from {tableName} order by Id DESC LIMIT {count}";

            return SqliteDataAccess.LoadData<T>(sql, null);
        }

        public static IEnumerable<T> GetAll<T>(DbTables tableName)
        {
            string sql = $"select * from {tableName}";

            return SqliteDataAccess.LoadData<T>(sql, null);
        }
    }
}
