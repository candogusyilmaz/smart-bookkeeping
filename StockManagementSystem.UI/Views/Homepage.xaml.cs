using StockManagementSystem.UI.ViewModels;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : UserControl
    {
        public HomepageViewModel ViewModel { get; set; }
        public Homepage()
        {
            InitializeComponent();

            ViewModel = new HomepageViewModel();

            this.DataContext = ViewModel;
        }
    }
}
