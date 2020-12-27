using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockManagementSystem.UI.ViewModels
{
    public class SalesListViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private readonly List<SaleDTO> Sales;
        public ObservableCollection<SaleDTO> FilteredSales { get; set; }
        public DateTime DisplayDateStart { get; set; }
        public DateTime DisplayDateEnd { get; set; }

        #region Filter Properties

        private string _totalAmount;
        public string TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; OnPropertyChanged(); }
        }


        private DateTime _dateHighFilter;
        public DateTime DateHighFilter
        {
            get { return _dateHighFilter; }
            set
            {
                _dateHighFilter = value.AddSeconds(86399);
                FilterSales();
            }
        }

        private DateTime _dateLowFilter;
        public DateTime DateLowFilter
        {
            get { return _dateLowFilter; }
            set
            {
                _dateLowFilter = value;
                FilterSales();
            }
        }

        private decimal _priceLowFilter;
        public decimal PriceLowFilter
        {
            get { return _priceLowFilter; }
            set
            {
                _priceLowFilter = value;
                FilterSales();
            }
        }

        private decimal _priceHighFilter;
        public decimal PriceHighFilter
        {
            get { return _priceHighFilter; }
            set
            {
                _priceHighFilter = value;
                FilterSales();
            }
        }

        private string _clientNameFilter;
        public string ClientNameFilter
        {
            get { return _clientNameFilter; }
            set
            {
                _clientNameFilter = value;
                FilterSales();
            }
        }

        #endregion

        public SalesListViewModel()
        {
            Sales = new List<SaleDTO>();

            foreach (var item in CommonService.GetAll<SaleModel>(DbTables.Sales))
                Sales.Add(new SaleDTO(item));

            DisplayDateStart = DateTime.Now;
            DisplayDateEnd = DateTime.Now;

            if (Sales.Any())
            {
                DisplayDateStart = Sales.OrderBy(s => s.Sale.TransactionDate).FirstOrDefault().Sale.TransactionDate;
                DisplayDateEnd = Sales.OrderByDescending(s => s.Sale.TransactionDate).FirstOrDefault().Sale.TransactionDate;
            }

            FilteredSales = new ObservableCollection<SaleDTO>(Sales);
        }

        private void FilterSales()
        {
            FilteredSales.Clear();

            var sales = Sales.Where(s => s.Client.FullName.Contains(ClientNameFilter));

            if (PriceLowFilter <= PriceHighFilter && PriceHighFilter + PriceLowFilter > 1)
            {
                sales = sales.Where(s => PriceLowFilter <= s.Sale.Price && s.Sale.Price <= PriceHighFilter);
            }

            if (DateLowFilter <= DateHighFilter)
            {
                sales = sales.Where(s => DateLowFilter <= s.Sale.TransactionDate && s.Sale.TransactionDate <= DateHighFilter);
            }

            foreach (var item in sales)
                FilteredSales.Add(item);

            TotalAmount = FilteredSales.Sum(s => s.Sale.Price).ConvertToCurrencyString();
        }
    }
}
