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
    /// Interaction logic for IncomesView.xaml
    /// </summary>
    public partial class IncomesView : UserControl
    {
        public ObservableCollection<IncomeModel> IncomeList { get; set; }
        public IncomesView()
        {
            InitializeComponent();

            this.DataContext = this;

            IncomeList = new ObservableCollection<IncomeModel>();
            foreach (var item in IncomeService.GetAll())
                IncomeList.Add(item);

            UpdateDateInformation(DateTime.Now);
        }

        private void UpdateDateInformation(DateTime date)
        {
            var monthData = IncomeList.Where(s => s.Date.Month == date.Month && s.Date.Year == date.Year).ToList();

            decimal monthTotalAmount = monthData.Sum(s => s.Amount);

            string monthTotal = $"{date.GetMonthString()}, {date.Year} : {monthTotalAmount.ConvertToCurrencyString()}";

            blockMonthTotal.Text = monthTotal;
        }

        private void NewIncome(object sender, RoutedEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new IncomeWindow());
            window.ShowDialog();

            if (((IncomeWindow)window.NextWindow).ChangesSaved)
            {
                IncomeList.Add(CommonService.GetLastInsertedRow<IncomeModel>(DbTables.Incomes).First());

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