using StockManagementSystem.Library;
using System.Windows;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductModel Product { get; set; }
        public bool ChangesSaved { get; set; }
        public ProductWindow()
        {
            InitializeComponent();
            ChangesSaved = false;
        }

        public ProductWindow(ProductModel model)
        {
            InitializeComponent();
            Product = model;
            LoadProduct();
        }

        private void LoadProduct()
        {
            txtProductName.Text = Product.ProductName;
            nbUnitSalePrice.Value = (double)Product.UnitSalePrice;
            nbUnitBuyPrice.Value = (double)Product.UnitBuyPrice;
            nbCount.Value = Product.Count;
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (Product == null) Product = new ProductModel();

            try
            {
                Product.ProductName = txtProductName.Text;
                Product.UnitBuyPrice = (decimal)nbUnitBuyPrice.Value;
                Product.UnitSalePrice = (decimal)nbUnitSalePrice.Value;
                Product.Count = (int)nbCount.Value;
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

            ProductService.SaveProduct(Product);

            MessageBox.Show("Ürün başarıyla kayıt edildi.", "Müşteri Kayıt", MessageBoxButton.OK, MessageBoxImage.Information);
            ChangesSaved = true;
            this.Close();
        }
    }
}
