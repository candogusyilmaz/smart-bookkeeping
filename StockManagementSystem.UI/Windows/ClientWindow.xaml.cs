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


            try
            {
                Client.Attachment = attachment;
            }
            catch { isValid = false; }

            return isValid;
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
