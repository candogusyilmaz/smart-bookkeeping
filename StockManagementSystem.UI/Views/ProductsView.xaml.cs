using StockManagementSystem.Library;
using StockManagementSystem.UI.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ObservableCollection<ProductModel> ProductList { get; set; }

        public ProductsView()
        {
            InitializeComponent();

            this.DataContext = this;

            ProductList = new ObservableCollection<ProductModel>();
            ProductService.GetAll().ForEach(s => ProductList.Add(s));
        }

        private void NewProduct(object sender, RoutedEventArgs e)
        {
            //DropShadowWindow dropShadowWindow = new DropShadowWindow();
            //dropShadowWindow.Show();

            //ProductWindow window = new ProductWindow();
            //window.Owner = Application.Current.MainWindow;
            //window.ShowDialog();

            //dropShadowWindow.Close();

            DropShadowWindow window = new DropShadowWindow(new ProductWindow());
            window.ShowDialog();

            if (((ProductWindow)window.NextWindow).ChangesSaved)
            {
                ProductList.Add(CommonService.GetLastInsertedRow<ProductModel>(DbTables.Products).First());
            }
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            var model = dataGrid.SelectedItem;

            if (model == null) return;

            DropShadowWindow window = new DropShadowWindow(new ProductWindow((ProductModel)model));
            window.ShowDialog();

            if (((ProductWindow)window.NextWindow).ChangesSaved)
                CollectionViewSource.GetDefaultView(ProductList).Refresh();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var model = (ProductModel)dataGrid.SelectedItem;

            if (e.Key == Key.Enter)
            {
                try
                {
                    model.Count = int.Parse(((TextBox)sender).Text);
                }
                catch
                {
                    ((TextBox)sender).Text = model.Count.ToString();
                    ((TextBox)sender).IsReadOnly = true;
                    return;
                }

                ((TextBox)sender).IsReadOnly = true;

                ProductService.ChangeProductCount(model);

                CollectionViewSource.GetDefaultView(ProductList).Refresh();
            }
            else if (e.Key == Key.Escape)
            {
                ((TextBox)sender).Text = model.Count.ToString();
                ((TextBox)sender).IsReadOnly = true;
            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).IsReadOnly = false;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).IsReadOnly = true;
        }
    }
}
