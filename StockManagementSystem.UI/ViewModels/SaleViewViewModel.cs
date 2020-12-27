using StockManagementSystem.Library;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockManagementSystem.UI.ViewModels
{
    public class SaleViewViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        public BindingList<ShoppingCartViewModel> ShoppingCart { get; set; }

        private ClientModel _client;

        public ClientModel Client
        {
            get { return _client; }
            set
            {
                _client = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ClientPaymentsTotal));
                OnPropertyChanged(nameof(ClientDelayedPaymentsTotal));
            }
        }

        private string _saleDescription;

        public string SaleDescription
        {
            get { return _saleDescription; }
            set { _saleDescription = value; }
        }


        public decimal ClientPaymentsTotal
        {
            get
            {
                if (Client == null) return 0;
                return ClientPaymentService.GetClientPayments(Client).Where(s => s.Type != 0).Sum(s => s.Amount);
            }
        }

        public decimal ClientDelayedPaymentsTotal
        {
            get
            {
                if (Client == null) return 0;
                return ClientPaymentService.GetClientPayments(Client).Where(s => s.Type == 0).Sum(s => s.Amount);
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return ShoppingCart.Sum(s => s.TotalPrice);
            }
        }

        public decimal TotalPriceWithDiscount
        {
            get
            {
                return ShoppingCart.Sum(s => s.TotalPrice) - CalculateDiscount();
            }
        }

        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPriceWithDiscount));
            }
        }

        private int _discountType;
        public int DiscountType
        {
            get { return _discountType; }
            set
            {
                _discountType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPriceWithDiscount));
            }
        }

        private int _paymentType;
        public int PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }

        private decimal _perPayForMonth;
        public decimal PerPayForMonth
        {
            get { return _perPayForMonth; }
            set
            {
                _perPayForMonth = value;
                OnPropertyChanged();
            }
        }

        private int _installmentCount;
        public int InstallmentCount
        {
            get
            {
                return _installmentCount < 1 ? 1 : _installmentCount;
            }
            set
            {
                _installmentCount = value;
                CalculatePerPayForMonth();
            }
        }

        public SaleViewViewModel()
        {
            ShoppingCart = new BindingList<ShoppingCartViewModel>();
            ShoppingCart.ListChanged += delegate
            {
                OnPropertyChanged(nameof(ShoppingCart));
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(TotalPriceWithDiscount));
            };
        }

        private decimal CalculateDiscount()
        {
            if (DiscountType == 0)
            {
                // discount by percent
                return TotalPrice * (Discount / 100);
            }
            else
            {
                // discount normal
                return Discount;
            }
        }

        public void CalculatePerPayForMonth()
        {
            PerPayForMonth = (TotalPriceWithDiscount / InstallmentCount);
        }
    }
}
