using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ClientPaymentsArchive.xaml
    /// </summary>
    public partial class ClientPaymentsArchive : UserControl
    {
        public ClientPaymentsArchiveViewModel ViewModel { get; set; }
        public ClientPaymentsArchive()
        {
            InitializeComponent();

            ViewModel = new ClientPaymentsArchiveViewModel();

            this.DataContext = ViewModel;

            Reset();
        }

        private void Reset()
        {
            asbName.Text = "";
            nbLow.Text = "";
            nbHigh.Text = "";
            dtLow.SelectedDate = ViewModel.DisplayDateStart;
            dtHigh.SelectedDate = ViewModel.DisplayDateEnd;
        }

        private void Reset(object sender, System.Windows.RoutedEventArgs e)
        {
            Reset();
        }
    }
}
