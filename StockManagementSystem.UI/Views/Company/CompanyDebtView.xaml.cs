using StockManagementSystem.Library;
using StockManagementSystem.UI.Windows;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for CompanyDebtView.xaml
    /// </summary>
    public partial class CompanyDebtView : UserControl
    {
        public CompanyDebtViewModel ViewModel { get; set; }

        public CompanyDebtView(CompanyModel model)
        {
            InitializeComponent();

            ViewModel = new CompanyDebtViewModel(model);

            this.DataContext = ViewModel;

            Reset();
        }

        private void Reset()
        {
            asbDescription.Text = "";
            nbLow.Text = "";
            nbHigh.Text = "";
            dtLow.SelectedDate = ViewModel.DisplayDateStart;
            dtHigh.SelectedDate = ViewModel.DisplayDateEnd;
        }


        private void NewCompanyPayment(object sender, RoutedEventArgs e)
        {
            DropShadowWindow window = new DropShadowWindow(new CompanyPaymentWindow(ViewModel.Company));
            window.ShowDialog();

            if (((CompanyPaymentWindow)window.NextWindow).ChangesSaved)
                ViewModel.InsertLastRecord();
        }

        private void SaveAttachment(object sender, RoutedEventArgs e)
        {
            if (((CompanyPaymentModel)dataGrid.SelectedItem).Attachment == null) return;

            Microsoft.Win32.SaveFileDialog ofd = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Ek'i kaydedin",
                Filter = "Image Files| *.jpg, *.jpeg"
            };

            bool? result = ofd.ShowDialog();


            if (result != true)
                return;

            ((CompanyPaymentModel)dataGrid.SelectedItem).Attachment.SaveBitmap(ofd.FileName);
        }

        private void FindSymbolIcon_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.IsSearchPaneOpen = !ViewModel.IsSearchPaneOpen;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            Reset();
        }
    }
}
