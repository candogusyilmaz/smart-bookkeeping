using ModernWpf.Controls;
using StockManagementSystem.UI.Views;
using StockManagementSystem.UI.Windows;
using System.Collections.Generic;
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

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var navItems = new List<NavigationViewItem>()
            {
                new NavigationViewItem() { Content = "Anasayfa", Tag = new Homepage(), Icon = new SymbolIcon(Symbol.Home), },
                new NavigationViewItem() { Content = "Yeni Satış", Tag = new SaleView(), Icon = new SymbolIcon(Symbol.Edit) },
                new NavigationViewItem() { Content = "Ürünler", Tag = new ProductsView(), Icon = new SymbolIcon(Symbol.AllApps) },
                new NavigationViewItem() { Content = "Müşteriler", Tag = new ClientsView(), Icon = new SymbolIcon(Symbol.People) },
                new NavigationViewItem() { Content = "Firmalar", Tag = new CompaniesView(), Icon = new SymbolIcon(Symbol.Globe) },
                new NavigationViewItem() { Content = "Gelirler", Tag = new IncomesView(), Icon = new SymbolIcon(Symbol.Add) },
                new NavigationViewItem() { Content = "Giderler", Tag = new ExpensesView(), Icon = new SymbolIcon(Symbol.Remove) },
                new NavigationViewItem() { Content = "Satış Arşivi", Tag = new SalesView(), Icon = new SymbolIcon(Symbol.List) },
                new NavigationViewItem() { Content = "Ödeme Arşivi", Tag = new ClientPaymentsArchive(), Icon = new SymbolIcon(Symbol.BrowsePhotos) },
            };

            NavigationView.MenuItemsSource = navItems;

            NavigationView.SelectedItem = navItems[0];
        }
        private void NavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                var window = new DropShadowWindow(new SettingsWindow());
                window.ShowDialog();
            }
            else
            {
                var item = sender.SelectedItem as NavigationViewItem;

                contentFrame.Navigate(item.Tag.GetType());
            }
        }
    }
}
