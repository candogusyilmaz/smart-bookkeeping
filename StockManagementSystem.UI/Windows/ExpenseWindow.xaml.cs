using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.Windows;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for ExpenseWindow.xaml
    /// </summary>
    public partial class ExpenseWindow : Window
    {
        public List<ExpenseModel> Expenses { get; set; }

        public bool ChangesSaved { get; set; } = false;
        public ExpenseWindow()
        {
            InitializeComponent();

            spInstallment.Visibility = Visibility.Collapsed;

            Expenses = new List<ExpenseModel>();
        }

        private void PaymentTypeChanged(object sender, RoutedEventArgs e)
        {
            if (cmbType.SelectedIndex == 0)
            {
                spInstallment.Visibility = Visibility.Visible;
            }
            else
            {
                spInstallment.Visibility = Visibility.Collapsed;
                nbInstallment.Value = 1;
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (txtNote.Text.Length < 1) return false;
            if (nbAmount.Value < 1) return false;
            if (cmbType.SelectedItem == null) return false;
            if (dtDate.SelectedDate == null) return false;

            if (cmbType.SelectedIndex == 0 && dtDate.SelectedDate.Value.Day > 28) return false;

            try
            {
                decimal perPayForMonth = (decimal)(nbAmount.Value / nbInstallment.Value);
                DateTime startDate = (DateTime)dtDate.SelectedDate;
                string expenseNote = txtNote.Text;

                for (int i = 1; i <= (int)nbInstallment.Value; i++)
                {
                    var expense = new ExpenseModel();

                    if (cmbType.SelectedIndex == 0)
                        expense.Note = $"{expenseNote} (Toplam: {(decimal)nbAmount.Value} TL  Taksit: {i}/{(int)nbInstallment.Value})";
                    else
                        expense.Note = txtNote.Text;

                    expense.Type = cmbType.Text;
                    expense.Amount = perPayForMonth;
                    expense.Date = startDate;

                    Expenses.Add(expense);

                    startDate = startDate.AddMonths(1);
                }
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm())
            {
                MessageBox.Show("Lütfen formu kontrol ediniz.", "Uyarı!", MessageBoxButton
                    .OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var item in Expenses)
                ExpenseService.InsertExpense(item);

            MessageBox.Show("Giderler başarıyla oluşturuldu.", "Gider Oluşturuldu", MessageBoxButton.OK, MessageBoxImage.Information);
            ChangesSaved = true;
            this.Close();
        }
    }
}
