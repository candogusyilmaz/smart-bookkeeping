using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace StockManagementSystem.Library
{
    public static class SqliteDataAccess
    {
        public static List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "Default")
        {
            using (IDbConnection conn = new SQLiteConnection(DataAccessHelpers.LoadConnectionString(connectionName)))
            {
                var rows = conn.Query<T>(sqlStatement, parameters.ToDynamicParameters());

                return rows.ToList();
            }
        }

        public static T LoadSingleData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "Default")
        {
            using (IDbConnection conn = new SQLiteConnection(DataAccessHelpers.LoadConnectionString(connectionName)))
            {
                var row = conn.ExecuteScalar<T>(sqlStatement, parameters.ToDynamicParameters());

                return row;
            }
        }

        public static void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "Default")
        {
            using (IDbConnection conn = new SQLiteConnection(DataAccessHelpers.LoadConnectionString(connectionName)))
            {
                conn.Execute(sqlStatement, parameters.ToDynamicParameters());
            }
        }

        public static DynamicParameters ToDynamicParameters(this Dictionary<string, object> parameters)
        {
            if (parameters == null) return new DynamicParameters();

            DynamicParameters p = new DynamicParameters();

            parameters.ToList().ForEach(s => p.Add(s.Key, s.Value));

            return p;
        }

        public static void Backup(string filename)
        {
            File.Copy(@".\AppDb.db", filename + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        }
    }
}
