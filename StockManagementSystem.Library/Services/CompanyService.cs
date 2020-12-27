using System.Collections.Generic;
using System.Linq;

namespace StockManagementSystem.Library
{
    public static class CompanyService
    {
        public static decimal GetAllDebt()
        {
            string sql = "select sum(Amount) from CompanyPayments";

            return SqliteDataAccess.LoadSingleData<decimal>(sql, null);
        }
        public static CompanyModel GetCompany(int id)
        {
            string sql = "select * from Companies where Id=" + id;

            return SqliteDataAccess.LoadData<CompanyModel>(sql, null).FirstOrDefault();
        }

        public static List<CompanyModel> GetAll()
        {
            string sql = "select * from Companies";

            return SqliteDataAccess.LoadData<CompanyModel>(sql, null);
        }

        public static void InsertCompany(CompanyModel model)
        {
            string sql = "insert into Companies (CompanyName, Phone, Address, Note) " +
                         "values (@CompanyName, @Phone, @Address, @Note)";

            var parameters = new Dictionary<string, object>
            {
                { "@CompanyName", model.CompanyName},
                { "@Phone", model.Phone},
                { "@Address", model.Address},
                { "@Note", model.Note}
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }

        public static void UpdateCompany(CompanyModel model)
        {
            string sql = "update Companies set CompanyName=@CompanyName, Phone=@Phone, Address=@Address, Note=@Note " +
                         "where Id=@Id";

            var parameters = new Dictionary<string, object>
            {
                { "@Id", model.Id},
                { "@CompanyName", model.CompanyName},
                { "@Phone", model.Phone},
                { "@Address", model.Address},
                { "@Note", model.Note}
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }

        public static void DeleteCompany(int id)
        {
            string sql = "delete from Companies where Id=" + id;

            SqliteDataAccess.SaveData(sql, null);
        }

        public static void DeleteCompany(CompanyModel model)
        {
            string sql = "delete from Companies where Id=" + model.Id;

            SqliteDataAccess.SaveData(sql, null);
        }
    }
}
