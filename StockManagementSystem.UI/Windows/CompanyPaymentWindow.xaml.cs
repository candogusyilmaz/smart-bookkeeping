using StockManagementSystem.Library;
using System;
using System.Windows;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for CompanyPaymentWindow.xaml
    /// </summary>
    public partial class CompanyPaymentWindow : Window
    {
        public CompanyPaymentModel CompanyPayment { get; set; }

        public bool ChangesSaved { get; set; } = false;

        public CompanyPaymentWindow(CompanyModel model)
        {
            InitializeComponent();

            CompanyPayment = new CompanyPaymentModel();
            CompanyPayment.CompanyId = model.Id;
        }

        private byte[] attachment;
        private void ChooseAttachment(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Ek seçin",
                Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg"
            };

            bool? result = ofd.ShowDialog();


            if (result != true)
                return;

            attachment = ImageHelpers.ToByteArray(ofd.FileName);
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            try
            {
                CompanyPayment.Attachment = attachment;
                CompanyPayment.Amount = Convert.ToDecimal(txtAmount.Text);
                CompanyPayment.Note = txtNote.Text;
                CompanyPayment.Type = cmbType.SelectedIndex;
                CompanyPayment.TransactionDate = dtDate.SelectedDate.GetValueOrDefault();
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

            CompanyPaymentService.InsertCompanyPayment(CompanyPayment);

            MessageBox.Show("Firma işlemi başarıyla kayıt edildi.", "Firma İşlem Kayıt", MessageBoxButton.OK, MessageBoxImage.Information);
            ChangesSaved = true;
            this.Close();
        }
    }
}
