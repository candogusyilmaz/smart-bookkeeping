using StockManagementSystem.Library;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockManagementSystem.UI.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        public ProductModel Product { get; private set; }

        private int _count;
        public int Count
        {
            get { return _count; }
            private set
            {
                _count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return Product.UnitSalePrice * Count;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ShoppingCartViewModel(ProductModel model)
        {
            Product = model;
            Count = 1;
        }

        public void IncreaseCount(int value = 1)
        {
            if (Count < Product.Count)
                Count += value;
        }

        public void DecreaseCount(int value = 1)
        {
            if (Count > 0)
                Count -= value;
        }
    }
}
