using System.Collections.Generic;
using System.Linq;

namespace StockManagementSystem.Library
{
    public static class ClientService
    {
        public static ClientModel Get(int id)
        {
            string sql = $"select * from Clients where Id={id}";

            return SqliteDataAccess.LoadData<ClientModel>(sql, null).FirstOrDefault();
        }
        public static void InsertClient(ClientModel model)
        {
            string sql = "insert into Clients (FullName, IdentityNumber, Phone, Address, Note, Attachment) " +
                         "values (@FullName, @IdentityNumber, @Phone, @Address, @Note, @Attachment)";

            var parameters = new Dictionary<string, object>
            {
                { "@IdentityNumber", model.IdentityNumber},
                { "@FullName", model.FullName},
                { "@Phone", model.Phone},
                { "@Address", model.Address},
                { "@Note", model.Note},
                { "@Attachment", model.Attachment}
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }

        public static void UpdateClient(ClientModel model)
        {
            string sql = "update Clients set FullName=@FullName, IdentityNumber=@IdentityNumber, Phone=@Phone, Address=@Address, Note=@Note, Attachment=@Attachment " +
                         "where Id=@Id";

            var parameters = new Dictionary<string, object>
            {
                { "@Id", model.Id},
                { "@IdentityNumber", model.IdentityNumber},
                { "@FullName", model.FullName},
                { "@Phone", model.Phone},
                { "@Address", model.Address},
                { "@Note", model.Note},
                { "@Attachment", model.Attachment}
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }

        public static void DeleteClient(int Id)
        {
            string sql = "delete from Clients where Id=" + Id;

            SqliteDataAccess.SaveData(sql, null);
        }

        public static void DeleteClient(ClientModel model)
        {
            string sql = "delete from Clients where Id=" + model.Id;

            SqliteDataAccess.SaveData(sql, null);
        }
    }
}
