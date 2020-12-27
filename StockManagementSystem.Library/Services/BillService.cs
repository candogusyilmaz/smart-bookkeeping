using System.Collections.Generic;

namespace StockManagementSystem.Library
{
    public static class BillService
    {
        public static IEnumerable<BillModel> GetAll(SaleModel model)
        {
            string sql = $"select * from Bills where SaleId={model.Id}";

            return SqliteDataAccess.LoadData<BillModel>(sql, null);
        }

        public static void InsertBill(BillModel model)
        {
            string sql = "insert into Bills (SaleId, ProductId, ProductAmount, Price) values (@SaleId, @ProductId, @ProductAmount, @Price)";

            var parameters = new Dictionary<string, object>
            {
                { "@SaleId", model.SaleId},
                { "@ProductId", model.ProductId},
                { "@ProductAmount", model.ProductAmount},
                { "@Price", model.Price}
            };

            SqliteDataAccess.SaveData(sql, parameters);

            ProductService.ChangeProductCount(model.ProductId, model.ProductAmount);
        }
    }
}
