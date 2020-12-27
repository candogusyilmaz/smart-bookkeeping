using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Library
{
    public class BillModel
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int ProductAmount { get; set; }

        public decimal Price { get; set; }

        private ProductModel _product;
        public ProductModel Product
        {
            get
            {
                if (_product == null)
                    _product = ProductService.Get(ProductId);
                return _product;
            }
        }
    }
}
