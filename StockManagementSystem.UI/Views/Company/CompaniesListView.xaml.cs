using StockManagementSystem.Library;
using StockManagementSystem.UI.ViewModels;
using StockManagementSystem.UI.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for CompaniesView.xaml
    /// </summary>
    public partial class CompaniesListView : UserControl
    {
        public ObservableCollection<CompanyDTO> CompanyViewList { get; set; }

        public CompaniesListView()
        {
            InitializeComponent();

            this.DataContext = this;

            CompanyViewList = new ObservableCollection<CompanyDTO>();

            foreach (var item in CompanyService.GetAll())
                CompanyViewList.Add(new CompanyDTO(item));
        }

        private void RefreshDataGrid()
        {
            decimal totalAmount = 0;

            foreach (var item in CompanyViewList)
            {
                foreach (var payment in item.CompanyPayments ?? new ObservableCollection<CompanyPaymentModel>())
                    totalAmount += payment.Amount;
            }

            blockTotalDebt.Text = $"Toplam Borç : {totalAmount.ConvertToCurrencyString()}";
        }

        private void NewCompany(object sender, RoutedEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new CompanyWindow());
            window.ShowDialog();

            if (((CompanyWindow)window.NextWindow).ChangesSaved)
                CompanyViewList.Add(new CompanyDTO(CommonService.GetLastInsertedRow<CompanyModel>(DbTables.Companies).First()));
        }
    }
}
