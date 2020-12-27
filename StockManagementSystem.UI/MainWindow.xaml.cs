using ModernWpf.Controls;
using StockManagementSystem.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace StockManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //contentControl.Content = new SaleView();
        }

        private readonly ObservableCollection<NavLink> _navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Label = "Yeni Satış", Symbol = Symbol.Edit },
            new NavLink() { Label = "Ürün", Symbol = Symbol.AllApps },
            new NavLink() { Label = "Müşteri", Symbol = Symbol.People },
            new NavLink() { Label = "Firma", Symbol = Symbol.Globe },
            new NavLink() { Label = "Gelir", Symbol = Symbol.Add },
            new NavLink() { Label = "Gider", Symbol = Symbol.Remove },
            new NavLink() { Label = "Satış Arşivi", Symbol = Symbol.List },
            new NavLink() { Label = "Ödeme Arşivi", Symbol = Symbol.BrowsePhotos }
        };

        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var name = (e.ClickedItem as NavLink).Label;

            switch (name)
            {
                case "Yeni Satış":
                    contentControl.Content = new SaleView();
                    break;
                case "Ürün":
                    contentControl.Content = new ProductsView();
                    break;
                case "Müşteri":
                    contentControl.Content = new ClientsView();
                    break;
                case "Firma":
                    contentControl.Content = new CompaniesView();
                    break;
                case "Gelir":
                    contentControl.Content = new IncomesView();
                    break;
                case "Gider":
                    contentControl.Content = new ExpensesView();
                    break;
                case "Satış Arşivi":
                    contentControl.Content = new SalesView();
                    break;
                case "Ödeme Arşivi":
                    contentControl.Content = new ClientPaymentsArchive();
                    break;
            }
        }
    }

    public class NavLink
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
    }
}
