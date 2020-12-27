using StockManagementSystem.Library;
using System.Windows;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientModel Client { get; set; }
        public bool ChangesSaved { get; set; } = false;

        public ClientWindow()
        {
            InitializeComponent();
        }

        public ClientWindow(ClientModel model)
        {
            InitializeComponent();
            Client = model;

            LoadClient();
        }

        private void LoadClient()
        {
            txtIdentity.Text = Client.IdentityNumber;
            txtFullName.Text = Client.FullName;
            txtPhone.Text = Client.Phone;
            txtAddress.Text = Client.Address;
            txtNote.Text = Client.Note;
        }

        private bool ValidateForm()
        {
            bool isValid = txtFullName.Text.Length > 0;

            if (Client == null) Client = new ClientModel();

            Client.FullName = txtFullName.Text;
            Client.IdentityNumber = txtIdentity.Text;
            Client.Phone = txtPhone.Text;
            Client.Address = txtAddress.Text;
            Client.Note = txtNote.Text;

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

            if (!(Client.Id > 0))
            {
                ClientService.InsertClient(Client);
            }
            else
            {
                ClientService.UpdateClient(Client);
            }


            MessageBox.Show("Müşteri başarıyla kayıt edildi.", "Müşteri Kayıt", MessageBoxButton.OK, MessageBoxImage.Information);
            ChangesSaved = true;
            this.Close();
        }
    }
}
