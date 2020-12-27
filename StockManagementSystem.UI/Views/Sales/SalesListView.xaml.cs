using StockManagementSystem.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for SalesListView.xaml
    /// </summary>
    public partial class SalesListView : UserControl
    {
        public SalesListViewModel ViewModel { get; set; }
        public SalesListView()
        {
            InitializeComponent();

            this.DataContext = this;

            ViewModel = new SalesListViewModel();

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

        private void Reset(object sender, RoutedEventArgs e)
        {
            Reset();
        }
    }
}
