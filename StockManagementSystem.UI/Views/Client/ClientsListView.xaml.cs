using StockManagementSystem.Library;
using StockManagementSystem.UI.Windows;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ClientsView.xaml
    /// </summary>
    public partial class ClientsListView : UserControl
    {
        public BindingList<ClientModel> ClientList { get; set; }
        public ClientsListView()
        {
            InitializeComponent();

            this.DataContext = this;

            ClientList = new BindingList<ClientModel>();

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

        private void EditClient(object sender, RoutedEventArgs e)
        {
            var selectedClient = dataGrid.SelectedItem as ClientModel;

            if (selectedClient == null) return;

            DropShadowWindow window = new DropShadowWindow(new ClientWindow(selectedClient));
            window.ShowDialog();

            if (((ClientWindow)window.NextWindow).ChangesSaved)
            {
                CollectionViewSource.GetDefaultView(ClientList).Refresh();
            }
        }
        private void SaveAttachment(object sender, RoutedEventArgs e)
        {

            if (((ClientModel)dataGrid.SelectedItem).Attachment == null) return;

            Microsoft.Win32.SaveFileDialog ofd = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Ek'i kaydedin",
                Filter = "Image Files| *.jpg, *.jpeg"
            };

            bool? result = ofd.ShowDialog();


            if (result != true)
                return;

            ((ClientModel)dataGrid.SelectedItem).Attachment.SaveBitmap(ofd.FileName);
        }
    }
}
