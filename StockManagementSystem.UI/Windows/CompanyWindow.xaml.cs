using StockManagementSystem.Library;
using System.Windows;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : Window
    {
        public CompanyModel Company { get; set; }

        public bool ChangesSaved { get; set; }
        public CompanyWindow()
        {
            InitializeComponent();

            ChangesSaved = false;
        }

        public CompanyWindow(CompanyModel model)
        {
            InitializeComponent();

            ChangesSaved = false;
            Company = model;
            LoadCompany();
        }

        private void LoadCompany()
        {
            txtCompanyName.Text = Company.CompanyName;
            txtPhone.Text = Company.Phone;
            txtAddress.Text = Company.Address;
            txtNote.Text = Company.Note;
        }

        private bool ValidateForm()
        {
            bool isValid = txtCompanyName.Text.Length > 0;

            if (Company == null) Company = new CompanyModel();

            Company.CompanyName = txtCompanyName.Text;
            Company.Phone = txtPhone.Text;
            Company.Address = txtAddress.Text;
            Company.Note = txtNote.Text;

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

            if (!(Company.Id > 0))
            {
                CompanyService.InsertCompany(Company);
            }
            else
            {
                CompanyService.UpdateCompany(Company);
            }


            MessageBox.Show("Firma başarıyla kayıt edildi.", "Müşteri Kayıt", MessageBoxButton.OK, MessageBoxImage.Information);
            ChangesSaved = true;
            this.Close();
        }
    }
}
