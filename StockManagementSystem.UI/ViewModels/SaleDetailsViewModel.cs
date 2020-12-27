using StockManagementSystem.Library;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StockManagementSystem
{
    public class SaleDetailsViewModel : BaseViewModel
    {
        private readonly IEnumerable<BillModel> Bills;
        public ObservableCollection<BillModel> FilteredBills { get; set; }
        public SaleModel Sale { get; set; }
        public decimal TotalAmount { get; set; }

        #region Filters properties

        private string _productNameFilter;
        public string ProductNameFilter
        {
            get { return string.IsNullOrWhiteSpace(_productNameFilter) ? "" : _productNameFilter; }
            set { _productNameFilter = value; FilterBills(); }
        }

        private decimal _priceLowFilter;
        public decimal PriceLowFilter
        {
            get { return _priceLowFilter; }
            set { _priceLowFilter = value; FilterBills(); }
        }

        private decimal _priceHighFilter;
        public decimal PriceHighFilter
        {
            get { return _priceHighFilter; }
            set { _priceHighFilter = value; FilterBills(); }
        }

        #endregion

        public SaleDetailsViewModel(SaleModel model)
        {
            Sale = model;
            Bills = BillService.GetAll(Sale);
            FilteredBills = new ObservableCollection<BillModel>(Bills);
        }

        private void FilterBills()
        {
            FilteredBills.Clear();

            var bills = Bills.Where(s => s.Product.ProductName.Contains(ProductNameFilter));

            if (PriceLowFilter <= PriceHighFilter && PriceHighFilter + PriceLowFilter > 1)
            {
                bills = bills.Where(s => PriceLowFilter <= s.Price && s.Price <= PriceHighFilter);
            }

            foreach (var item in bills)
                FilteredBills.Add(item);

            TotalAmount = FilteredBills.Sum(s => s.Price);
        }
    }
}
