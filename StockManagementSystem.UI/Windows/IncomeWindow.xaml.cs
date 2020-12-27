using StockManagementSystem.Library;
using System;
using System.Windows;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for IncomeWindow.xaml
    /// </summary>
    public partial class IncomeWindow : Window
    {
        public IncomeModel Income { get; set; }
        public bool ChangesSaved { get; set; } = false;

        public IncomeWindow()
        {
            InitializeComponent();

            cmbType.SelectedIndex = 0;
            cmbType.IsEnabled = false;

            dtDate.SelectedDate = DateTime.Now;
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (txtNote.Text.Length < 1) return false;
            if (nbAmount.Value < 1) return false;
            if (cmbType.SelectedItem == null) return false;
            if (dtDate.SelectedDate == null) return false;

            try
            {
                if (Income == null) Income = new IncomeModel();

                Income.Note = txtNote.Text;
                Income.Type = cmbType.Text;
                Income.Amount = (decimal)nbAmount.Value;
                Income.Date = (DateTime)dtDate.SelectedDate;
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

            IncomeService.InsertIncome(Income);

            MessageBox.Show("Gelir başarıyla oluşturuldu.", "Gelir Oluşturuldu", MessageBoxButton.OK, MessageBoxImage.Information);
            ChangesSaved = true;
            this.Close();
        }
    }
}
