using StockManagementSystem.Library;
using System.Windows;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for SaleDetailsView.xaml
    /// </summary>
    public partial class SaleDetailsView : UserControl
    {
        public SaleDetailsView(SaleModel model)
        {
            InitializeComponent();

            this.DataContext = new SaleDetailsViewModel(model);
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            asbProductName.Text = "";
            nbLow.Text = "";
            nbHigh.Text = "";
        }
    }
}
