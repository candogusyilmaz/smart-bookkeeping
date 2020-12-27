using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StockManagementSystem.UI
{
    public class ClientPaymentsArchiveViewModel : BaseViewModel
    {
        private IEnumerable<ClientPaymentModel> ClientPayments;
        public ObservableCollection<ClientPaymentModel> FilteredClientPayments { get; set; }

        public DateTime DisplayDateStart { get; set; }
        public DateTime DisplayDateEnd { get; set; }

        private string _clientNameFilter;
        public string ClientNameFilter
        {
            get { return _clientNameFilter; }
            set { _clientNameFilter = value; FilterClientPayments(); }
        }


        private decimal _priceLowFilter;
        public decimal PriceLowFilter
        {
            get { return _priceLowFilter; }
            set { _priceLowFilter = value; FilterClientPayments(); }
        }

        private decimal _priceHighFilter;
        public decimal PriceHighFilter
        {
            get { return _priceHighFilter; }
            set { _priceHighFilter = value; FilterClientPayments(); }
        }

        private DateTime _dateLowFilter;
        public DateTime DateLowFilter
        {
            get { return _dateLowFilter; }
            set { _dateLowFilter = value; FilterClientPayments(); }
        }

        private DateTime _dateHighFilter;
        public DateTime DateHighFilter
        {
            get { return _dateHighFilter.AddSeconds(86349); }
            set { _dateHighFilter = value; FilterClientPayments(); }
        }

        public decimal TotalAmount { get; set; }

        public ClientPaymentsArchiveViewModel()
        {
            ClientPayments = CommonService.GetAll<ClientPaymentModel>(DbTables.ClientPayments);
            FilteredClientPayments = new ObservableCollection<ClientPaymentModel>(ClientPayments);

            if (ClientPayments.Any())
            {
                DisplayDateStart = ClientPayments.OrderBy(s => s.Date).First().Date;
                DisplayDateEnd = ClientPayments.OrderByDescending(s => s.Date).First().Date;
            }
        }

        private void FilterClientPayments()
        {
            FilteredClientPayments.Clear();

            var sales = ClientPayments.Where(s => s.Client.FullName.Contains(ClientNameFilter));

            if (PriceLowFilter <= PriceHighFilter && PriceHighFilter + PriceLowFilter > 1)
            {
                sales = sales.Where(s => PriceLowFilter <= s.Amount && s.Amount <= PriceHighFilter);
            }

            if (DateLowFilter <= DateHighFilter)
            {
                sales = sales.Where(s => DateLowFilter <= s.Date && s.Date <= DateHighFilter);
            }

            foreach (var item in sales)
                FilteredClientPayments.Add(item);

            TotalAmount = FilteredClientPayments.Sum(s => s.Amount);
        }
    }
}
