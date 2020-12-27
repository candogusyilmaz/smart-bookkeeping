using StockManagementSystem.UI.ViewModels;
using StockManagementSystem.UI.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for CompanyDebtView.xaml
    /// </summary>
    public partial class CompanyDebtView : UserControl
    {
        public CompanyDTO CompanyDTO { get; set; }

        public CompanyDebtView(CompanyDTO model)
        {
            InitializeComponent();

            this.DataContext = this;

            if (model == null)
                return;

            CompanyDTO = model;

            blockCompanyName.Text = CompanyDTO.Company.CompanyName;
            UpdateDebtInformation();
        }

        private void UpdateDebtInformation()
        {
            string totalDebt = $"Toplam Ödeme : {CompanyDTO.CompanyPaidAmount.ConvertToCurrencyString()} / {CompanyDTO.CompanyDebtAmount.ConvertToCurrencyString()}";

            string currentMonthDebt = $"{DateTime.Now.GetMonthString()} {DateTime.Now.Year} : {CompanyDTO.CurrentMonthPaidAmount.ConvertToCurrencyString()} / {CompanyDTO.CurrentMonthDebtAmount.ConvertToCurrencyString()}";

            var nextMonth = (DateTime.Now).AddMonths(1);
            string nextMonthDebt = $"{(nextMonth).GetMonthString()} {nextMonth.Year} : {CompanyDTO.NextMonthPaidAmount.ConvertToCurrencyString()} / {CompanyDTO.NextMonthDebtAmount.ConvertToCurrencyString()}";

            blockTotal.Text = totalDebt;
            blockCurrentMonth.Text = currentMonthDebt;
            blockNextMonth.Text = nextMonthDebt;
        }

        private void NewCompanyPayment(object sender, RoutedEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new CompanyPaymentWindow(CompanyDTO.Company));
            window.ShowDialog();

            if (((CompanyPaymentWindow)window.NextWindow).ChangesSaved)
                CompanyDTO.InsertLastRecord();
        }

        private void ShowAttachment(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("yapım aşaması");
        }

        private void OpenCompanyWindow(object sender, MouseButtonEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new CompanyWindow(CompanyDTO.Company));
            window.ShowDialog();

            if (((CompanyWindow)window.NextWindow).ChangesSaved == false) return;

            CompanyDTO.Company = ((CompanyWindow)window.NextWindow).Company;
            blockCompanyName.Text = CompanyDTO.Company.CompanyName;
        }
    }
}
