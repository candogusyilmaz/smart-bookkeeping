using StockManagementSystem.Library;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockManagementSystem.UI.ViewModels
{
    public class SaleDTO : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public SaleModel Sale { get; private set; }

        public ClientModel Client { get; private set; }

        public SaleDTO(SaleModel model)
        {
            Sale = model;
            Client = ClientService.Get(Sale.ClientId);
        }
    }
}
