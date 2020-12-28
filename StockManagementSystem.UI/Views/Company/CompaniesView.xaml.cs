using StockManagementSystem.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for CompanyView.xaml
    /// </summary>
    public partial class CompaniesView : UserControl
    {
        private CompaniesListView companiesListView;
        private CompanyDebtView companyDebtView;

        private readonly Style rowStyle;

        public CompaniesView()
        {
            InitializeComponent();

            companiesListView = new CompaniesListView();

            rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(Row_DoubleClick)));

            companiesListView.dataGrid.RowStyle = rowStyle;

            content.Content = companiesListView;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (companiesListView.dataGrid.SelectedItem == null)
                return;

            companyDebtView = new CompanyDebtView(((CompanyDTO)companiesListView.dataGrid.SelectedItem).Company);
            companyDebtView.ShowCompanies.Click += ShowCompanies_Click;

            content.Content = companyDebtView;
        }

        private void ShowCompanies_Click(object sender, RoutedEventArgs e)
        {
            companiesListView = new CompaniesListView();
            companiesListView.dataGrid.RowStyle = rowStyle;

            content.Content = companiesListView;
        }
    }
}
