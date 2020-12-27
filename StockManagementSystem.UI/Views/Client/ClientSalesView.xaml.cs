using StockManagementSystem.Library;
using StockManagementSystem.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ClientSalesView.xaml
    /// </summary>
    public partial class ClientSalesView : UserControl
    {
        public ClientSalesViewModel ViewModel { get; set; }
        public ClientSalesView(ClientModel client)
        {
            InitializeComponent();

            this.DataContext = this;

            ViewModel = new ClientSalesViewModel(client);
        }

        private void ChangeMonth(object sender, RoutedEventArgs e)
        {

        }
    }
}
