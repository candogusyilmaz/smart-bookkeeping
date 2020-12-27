using System.Collections.Generic;
using System.Linq;

namespace StockManagementSystem.Library
{
    public static class SaleService
    {
        /// <summary>
        /// Returns the SaleId after insert
        /// </summary>
        public static int InsertSale(SaleModel model)
        {
            string sql = "insert into Sales (ClientId, TransactionDate, Price, Note) values (@ClientId, @TransactionDate, @Price, @Note)";

            var parameters = new Dictionary<string, object>
            {
                { "@ClientId", model.ClientId},
                { "@TransactionDate", model.TransactionDate},
                { "@Price", model.Price},
                { "@Note", model.Note}
            };

            SqliteDataAccess.SaveData(sql, parameters);

            return CommonService.GetLastInsertedRow<SaleModel>(DbTables.Sales).First().Id;
        }

        public static IEnumerable<SaleModel> GetSales(ClientModel client)
        {
            string sql = $"select * from Sales where ClientId={client.Id}";

            return SqliteDataAccess.LoadData<SaleModel>(sql, null);
        }
    }
}
