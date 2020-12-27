using StockManagementSystem.Library;
using StockManagementSystem.UI.Windows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ClientPaymentsView.xaml
    /// </summary>
    public partial class ClientPaymentsView : UserControl
    {
        public ClientPaymentsViewModel ViewModel { get; set; }

        public ClientPaymentsView(ClientModel model)
        {
            InitializeComponent();

            ViewModel = new ClientPaymentsViewModel(model);

            this.DataContext = ViewModel;
        }

        private void NewClientPayment(object sender, RoutedEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new ClientPaymentWindow(ViewModel.Client));
            window.ShowDialog();

            if (((ClientPaymentWindow)window.NextWindow).ChangesSaved)
            {
                ViewModel.InsertLastRecords(((ClientPaymentWindow)window.NextWindow).ClientPayments.Count);
                ViewModel.ChangeMonthDetails(DateTime.Now);
            }
        }

        private void ChangeMonth(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ViewModel.ChangeMonthDetails(new DateTime(DateTime.Now.Year, Convert.ToInt32(item.Tag), 1));
        }

        private void ChangeStatusToPaid(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedClientPayment == null || ViewModel.SelectedClientPayment.Type != 0) return;

            var result = MessageBox.Show($"{ViewModel.SelectedClientPayment.Note}\n\n{ViewModel.SelectedClientPayment.Amount.ConvertToCurrencyString()} değerindeki veresiyeyi ödemek istiyor musunuz?", "Dikkat!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            ClientPaymentService.PayDelayedPayment(ViewModel.SelectedClientPayment);

            ViewModel.ChangeMonthDetails(DateTime.Now);
        }
    }
}
