using StockManagementSystem.UI.Windows;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for CompaniesView.xaml
    /// </summary>
    public partial class CompaniesListView : UserControl
    {
        public CompanyListViewModel ViewModel { get; set; }

        public CompaniesListView()
        {
            InitializeComponent();

            ViewModel = new CompanyListViewModel();

            this.DataContext = ViewModel;
        }

        private void NewCompany(object sender, RoutedEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new CompanyWindow());
            window.ShowDialog();

            if ((window.NextWindow as CompanyWindow).ChangesSaved)
                ViewModel.InsertLastRecord();
        }
    }
}
