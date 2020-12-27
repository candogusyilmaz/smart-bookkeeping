using System;
using System.Collections.Generic;

namespace StockManagementSystem.Library
{
    public static class ClientPaymentService
    {
        public static List<ClientPaymentModel> GetClientPayments(ClientModel model)
        {
            string sql = "select * from ClientPayments where ClientId=" + model.Id;

            return SqliteDataAccess.LoadData<ClientPaymentModel>(sql, null);
        }

        public static void InsertPayment(ClientPaymentModel model)
        {
            string sql = "insert into ClientPayments (ClientId, Amount, Type, Date, Note) " +
                         "values (@ClientId, @Amount, @Type, @Date, @Note)";

            var parameters = new Dictionary<string, object>
            {
                { "@ClientId", model.ClientId},
                { "@Amount", model.Amount},
                { "@Type", model.Type},
                { "@Date", model.Date},
                { "@Note", model.Note}
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }

        public static void PayDelayedPayment(ClientPaymentModel model)
        {
            model.Type = 2;
            model.Date = DateTime.Now;

            string sql = $"update ClientPayments set Type=@Type, Date=@Date where Id=@Id";

            var parameters = new Dictionary<string, object>()
            {
                {"@Id",model.Id },
                {"@Type",model.Type },
                {"@Date",model.Date }
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }
    }
}
