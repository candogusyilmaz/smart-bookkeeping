using StockManagementSystem.Library;
using StockManagementSystem.UI.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ExpensesView.xaml
    /// </summary>
    public partial class ExpensesView : UserControl
    {
        public ObservableCollection<ExpenseModel> ExpenseList { get; set; }
        public ExpensesView()
        {
            InitializeComponent();

            this.DataContext = this;

            ExpenseList = new ObservableCollection<ExpenseModel>();

            foreach (var item in CommonService.GetAll<ExpenseModel>(DbTables.Expenses))
                ExpenseList.Add(item);

            UpdateDateInformation(DateTime.Now);
        }

        private void UpdateDateInformation(DateTime date)
        {
            var monthData = ExpenseList.Where(s => s.Date.Month == date.Month && s.Date.Year == date.Year);

            decimal monthTotalAmount = monthData.Sum(s => s.Amount);

            string monthTotal = $"{date.GetMonthString()}, {date.Year} Toplamı : {monthTotalAmount.ConvertToCurrencyString()}";

            decimal creditCardAmount = monthData.Where(s => s.Type == PaymentTypeHelper.CreditCard).Sum(s => s.Amount);

            string creditCard = $"Kredi Kartı : {creditCardAmount.ConvertToCurrencyString()}";

            decimal cashAmount = monthTotalAmount - creditCardAmount;

            string cash = $"Nakit : {cashAmount.ConvertToCurrencyString()}";

            blockMonthTotal.Text = monthTotal;
            blockCreditCard.Text = creditCard;
            blockCash.Text = cash;
        }

        private void NewExpense(object sender, RoutedEventArgs e)
        {

            DropShadowWindow window = new DropShadowWindow(new ExpenseWindow());
            window.ShowDialog();

            if (((ExpenseWindow)window.NextWindow).ChangesSaved)
            {
                foreach (var item in CommonService.GetLastInsertedRow<ExpenseModel>(DbTables.Expenses, ((ExpenseWindow)window.NextWindow).Expenses.Count))
                    ExpenseList.Add(item);

                UpdateDateInformation(DateTime.Now);
            }
        }

        private void ChangeMonth(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            UpdateDateInformation(new DateTime(DateTime.Now.Year, Convert.ToInt32(item.Tag), 1));
        }
    }
}
