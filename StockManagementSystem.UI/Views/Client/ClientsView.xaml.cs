using StockManagementSystem.Library;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientsView : UserControl
    {
        private ClientsListView clientsListView;
        private ClientPaymentsView clientPaymentsView;
        private ClientSalesView clientSalesView;

        private readonly Style rowStyle = new Style(typeof(DataGridRow));

        public ClientsView()
        {
            InitializeComponent();

            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(Row_DoubleClick)));

            ChangeContent(GetClientsListView());
        }

        private void ShowClientSales_Click(object sender, RoutedEventArgs e)
        {
            ChangeContent(GetClientSalesView());
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeContent(GetClientPaymentsView());
        }

        private void BackToClientList_Click(object sender, RoutedEventArgs e)
        {
            ChangeContent(GetClientsListView());
        }

        private void ChangeContent(object view)
        {
            if (view != null)
                content.Content = view;
        }

        private ClientsListView GetClientsListView()
        {
            clientsListView = new ClientsListView();
            clientsListView.dataGrid.RowStyle = rowStyle;
            clientsListView.ShowClientSales.Click += ShowClientSales_Click;
            clientsListView.ShowClientPayments.Click += delegate { ChangeContent(GetClientPaymentsView()); };

            return clientsListView;
        }

        private ClientSalesView GetClientSalesView()
        {
            if (clientsListView.dataGrid.SelectedItem == null)
                return null;

            clientSalesView = new ClientSalesView((ClientModel)clientsListView.dataGrid.SelectedItem);
            clientSalesView.BackToClientList.Click += BackToClientList_Click;

            return clientSalesView;
        }

        private ClientPaymentsView GetClientPaymentsView()
        {
            if (clientsListView.dataGrid.SelectedItem == null)
                return null;

            clientPaymentsView = new ClientPaymentsView((ClientModel)clientsListView.dataGrid.SelectedItem);
            clientPaymentsView.BackToClientList.Click += BackToClientList_Click;

            return clientPaymentsView;
        }
    }
}
