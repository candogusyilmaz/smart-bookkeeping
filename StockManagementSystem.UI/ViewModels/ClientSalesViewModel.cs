using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockManagementSystem.UI.ViewModels
{
    public class ClientSalesViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private ClientModel _client;
        public ClientModel Client
        {
            get { return _client; }
            set
            {
                _client = value;
                ClientSales = SaleService.GetSales(_client);
                OnPropertyChanged();
            }
        }

        private IEnumerable<SaleModel> _clientSales;
        public IEnumerable<SaleModel> ClientSales
        {
            get { return _clientSales; }
            private set { _clientSales = value; OnPropertyChanged(); }
        }

        #region Total sale amounts

        private string _monthTotal;
        public string SelectedMonthTotalText
        {
            get { return _monthTotal; }
            set
            {
                _monthTotal = value;
                OnPropertyChanged();
            }
        }

        private string _creditCard;
        public string CreditCardTotalText
        {
            get { return _creditCard; }
            set
            {
                _creditCard = value;
                OnPropertyChanged();
            }
        }

        private string _cash;
        public string CashTotalText
        {
            get { return _cash; }
            set
            {
                _cash = value;
                OnPropertyChanged();
            }
        }

        private string _openAccount;
        public string OpenAccountText
        {
            get { return _openAccount; }
            set
            {
                _openAccount = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ClientSalesViewModel(ClientModel client)
        {
            this.Client = client;

            this.SelectedMonthTotalText = CalculateMonthTotal(DateTime.Now);
        }

        private string CalculateMonthTotal(DateTime date)
        {
            var monthTotal = ClientSales
                .Where(s => s.TransactionDate.Month == date.Month && s.TransactionDate.Year == date.Year)
                .Sum(s => s.Price);

            return $"{date.GetMonthString()}, {date.Year}: {monthTotal.ConvertToCurrencyString()}";
        }
    }
}
