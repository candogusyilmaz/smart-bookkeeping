using System.Collections.Generic;
using System.Linq;

namespace StockManagementSystem.Library
{
    public static class ProductService
    {
        public static ProductModel Get(int productId)
        {
            string sql = $"select * from Products where Id={productId}";

            return SqliteDataAccess.LoadData<ProductModel>(sql, null).FirstOrDefault();
        }

        public static List<ProductModel> GetAll()
        {
            string sql = "select * from Products";

            return SqliteDataAccess.LoadData<ProductModel>(sql, null);
        }

        public static void SaveProduct(ProductModel model)
        {
            string insertSql = "insert into Products (ProductName, UnitBuyPrice, UnitSalePrice, Count) " +
                         "values (@ProductName, @UnitBuyPrice, @UnitSalePrice, @Count)";

            string updateSql = "update Products set ProductName=@ProductName, UnitBuyPrice=@UnitBuyPrice, UnitSalePrice=@UnitSalePrice, Count=@Count " +
                               "where Id=@Id";

            var parameters = new Dictionary<string, object>
            {
                { "@ProductName", model.ProductName},
                { "@UnitBuyPrice", model.UnitBuyPrice},
                { "@UnitSalePrice", model.UnitSalePrice},
                { "@Count", model.Count}
            };

            if (model.Id > 0)
            {
                parameters.Add("@Id", model.Id);
                SqliteDataAccess.SaveData(updateSql, parameters);
            }
            else
            {
                SqliteDataAccess.SaveData(insertSql, parameters);
            }
        }

        public static void ChangeProductCount(ProductModel model)
        {
            string sql = $"update Products set Count={model.Count} where Id={model.Id}";

            SqliteDataAccess.SaveData(sql, null);
        }

        public static void ChangeProductCount(int productId, int count)
        {
            var product = Get(productId);
            product.Count -= count;

            ChangeProductCount(product);
        }
    }
}
