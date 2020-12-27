using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Library
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitSalePrice { get; set; }
        public decimal UnitBuyPrice { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
