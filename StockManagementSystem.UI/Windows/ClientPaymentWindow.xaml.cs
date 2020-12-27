using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for ClientPaymentWindow.xaml
    /// </summary>
    public partial class ClientPaymentWindow : Window
    {
        public List<ClientPaymentModel> ClientPayments { get; set; }

        public ClientModel Client { get; set; }

        public bool ChangesSaved { get; set; } = false;

        public ClientPaymentWindow(ClientModel model)
        {
            InitializeComponent();

            installmentStackPanel.Visibility = Visibility.Collapsed;
            Client = model;
        }

        private int selectedType;
        private void PaymentTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            string paymentType = (string)((ComboBoxItem)cmbType.SelectedItem).Content;

            if (paymentType == PaymentTypeHelper.CreditCard)
            {
                installmentStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                installmentStackPanel.Visibility = Visibility.Collapsed;
                dtDate.SelectedDate = DateTime.Now;
            }

            if (paymentType == PaymentTypeHelper.DelayedPayment)
                selectedType = 0;
            else if (paymentType == PaymentTypeHelper.CreditCard)
                selectedType = 1;
            else
                selectedType = 2;
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            try
            {
                if (ClientPayments == null) ClientPayments = new List<ClientPaymentModel>();

                if (txtNote.Text.Length < 1) return false;
                if (nbAmount.Value < 1) return false;
                if (cmbType.SelectedItem == null) return false;
                if (dtDate.SelectedDate == null) return false;
                if (cmbType.SelectedIndex == 0 && dtDate.SelectedDate.Value.Day > 28) return false;

                decimal perPayForMonth = (decimal)(nbAmount.Value / nbInstallment.Value);
                DateTime startDate = (DateTime)dtDate.SelectedDate;
                string paymentNote = txtNote.Text;

                for (int i = 1; i <= (int)nbInstallment.Value; i++)
                {
                    var payment = new ClientPaymentModel();

                    if (selectedType == 1 && cmbType.SelectedIndex == 0)
                        payment.Note = $"{paymentNote} (Toplam: {(decimal)nbAmount.Value} TL  Taksit: {i}/{(int)nbInstallment.Value})";
                    else if (selectedType == 0)
                        payment.Note = $"{paymentNote} (Veresiye)";
                    else
                        payment.Note = paymentNote;

                    payment.ClientId = Client.Id;
                    payment.Type = selectedType;
                    payment.Amount = perPayForMonth;
                    payment.Date = startDate;

                    ClientPayments.Add(payment);

                    startDate = startDate.AddMonths(1);
                }
            }
            catch
            {
                return false;
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

            foreach (var item in ClientPayments)
                ClientPaymentService.InsertPayment(item);

            MessageBox.Show("Müşteri ödeme kaydı başarıyla oluşturuldu.", "Ödeme Kaydı Oluşturuldu", MessageBoxButton.OK, MessageBoxImage.Information);
            ChangesSaved = true;
            this.Close();
        }
    }
}
