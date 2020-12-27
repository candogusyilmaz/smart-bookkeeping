using StockManagementSystem.Library;
using StockManagementSystem.UI.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ClientsView.xaml
    /// </summary>
    public partial class ClientsListView : UserControl
    {
        public ObservableCollection<ClientModel> ClientList { get; set; }
        public ClientsListView()
        {
            InitializeComponent();

            this.DataContext = this;

            ClientList = new ObservableCollection<ClientModel>();

            foreach (var item in CommonService.GetAll<ClientModel>(DbTables.Clients))
                ClientList.Add(item);
        }

        private void NewClient(object sender, RoutedEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new ClientWindow());
            window.ShowDialog();

            if (((ClientWindow)window.NextWindow).ChangesSaved)
            {
                ClientList.Add(CommonService.GetLastInsertedRow<ClientModel>(DbTables.Clients).First());
            }
        }
    }
}
