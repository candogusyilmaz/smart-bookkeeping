using ModernWpf.Controls;
using StockManagementSystem.Library;
using StockManagementSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockManagementSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for SaleView.xaml
    /// </summary>
    public partial class SaleView : UserControl
    {
        public SaleViewViewModel ViewModel { get; set; }

        public SaleView()
        {
            InitializeComponent();

            ViewModel = new SaleViewViewModel();

            this.DataContext = this;

            RefreshView();
        }

        private void RefreshView()
        {
            asbProductSearch.Text = null;
            asbClientSearch.Text = null;
            asbSaleDescription.Text = null;

            cmbPaymentType.SelectedIndex = 1; // cash
            cmbDiscountType.SelectedIndex = 1; // normal

            ViewModel.ShoppingCart.Clear();
            ViewModel.Client = null;
        }

        private void DiscountTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDiscountType.SelectedIndex == 0)
            {
                var inputScope = new InputScope();
                inputScope.Names.Add(new InputScopeName(InputScopeNameValue.Digits));

                nbDiscount.InputScope = inputScope;
                nbDiscount.Minimum = 0;
                nbDiscount.Value = 0;
                nbDiscount.Maximum = 100;

            }
            else
            {
                var inputScope = new InputScope();
                inputScope.Names.Add(new InputScopeName(InputScopeNameValue.CurrencyAmount));

                nbDiscount.InputScope = inputScope;
                nbDiscount.Minimum = 0;
                nbDiscount.Value = 0;
                nbDiscount.Maximum = (double)ViewModel.TotalPrice;
            }
        }

        private void PaymentTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            int paymentType = int.Parse((string)((ComboBoxItem)cmbPaymentType.SelectedItem).Tag);

            if (paymentType == 2) // cash
            {
                InstallmentPanel.Visibility = Visibility.Collapsed;

            }
            else // open account, credit card
            {
                InstallmentPanel.Visibility = Visibility.Visible;
            }
        }

        private void CreateSale(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm())
            {
                MessageBox.Show("Satış gerçekleştirilemedi!", "Dikkat!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // create sale info
            int saleId = InsertSale();

            // create bills for per product added
            InsertBills(saleId);

            // create client payment
            InsertClientPayments();

            MessageBox.Show($"{ViewModel.TotalPriceWithDiscount.ConvertToCurrencyString()} değerindeki satış işlemi {DateTime.Now.ToShortDateString()} tarihinde başarıyla gerçekleştirildi.", "Satış İşlemi Gerçekleştirildi!", MessageBoxButton.OK, MessageBoxImage.Information);

            RefreshView();
        }

        private bool ValidateForm()
        {
            if (ViewModel.ShoppingCart.Count < 1) return false;
            if (ViewModel.Client == null) return false;

            return true;
        }

        private int InsertSale()
        {
            SaleModel sale = new SaleModel();
            sale.ClientId = ViewModel.Client.Id;
            sale.Price = ViewModel.TotalPriceWithDiscount;
            sale.TransactionDate = DateTime.Now;
            sale.Note = ViewModel.SaleDescription;

            return SaleService.InsertSale(sale);
        }

        private void InsertBills(int saleId)
        {
            foreach (var item in ViewModel.ShoppingCart)
            {
                BillModel bill = new BillModel();
                bill.SaleId = saleId;
                bill.ProductId = item.Product.Id;
                bill.ProductAmount = item.Count;
                bill.Price = item.TotalPrice;

                BillService.InsertBill(bill);
            }
        }

        private void InsertClientPayments()
        {
            List<ClientPaymentModel> clientPayments = new List<ClientPaymentModel>();

            DateTime startDate = DateTime.Now;

            for (int i = 1; i <= ViewModel.InstallmentCount; i++)
            {
                var payment = new ClientPaymentModel();
                payment.Note = $"{DateTime.Now.ToShortDateString()} tarihinde satış yapıldı.";

                if (ViewModel.PaymentType != 2)
                {
                    payment.Note += $" ({i} / {ViewModel.InstallmentCount})";
                }

                payment.ClientId = ViewModel.Client.Id;
                payment.Type = int.Parse((string)((ComboBoxItem)cmbPaymentType.SelectedItem).Tag);

                ViewModel.CalculatePerPayForMonth();
                payment.Amount = ViewModel.PerPayForMonth;
                payment.Date = startDate;

                clientPayments.Add(payment);

                startDate = startDate.AddMonths(1);
            }

            foreach (var item in clientPayments)
            {
                ClientPaymentService.InsertPayment(item);
            }
        }

        #region Product autosuggestbox

        private ProductModel selectedProduct;

        private void asbProductSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchProducts(sender.Text);

                if (suggestions.Count > 0)
                    sender.ItemsSource = suggestions;
                else
                    sender.ItemsSource = new string[] { "Ürün bulunamadı" };
            }
        }
        private void asbProductSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (selectedProduct != args.SelectedItem as ProductModel)
                selectedProduct = args.SelectedItem as ProductModel;
        }

        private readonly List<ProductModel> products = CommonService.GetAll<ProductModel>(DbTables.Products).Where(s => s.Count > 0).ToList();
        private List<ProductModel> SearchProducts(string query)
        {
            var querySplit = query.Split(' ');
            var suggestions = products.Where(
                item =>
                {
                    bool flag = true;
                    foreach (string queryToken in querySplit)
                    {
                        if (item.ProductName.IndexOf(queryToken, StringComparison.CurrentCultureIgnoreCase) < 0)
                        {
                            flag = false;
                        }
                    }
                    return flag;
                });
            return suggestions.OrderByDescending(i => i.ProductName.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ThenBy(i => i.ProductName).ToList();
        }

        #endregion

        private void AddProductToDataGrid(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null) return;

            var shoppingCartItem = new ShoppingCartViewModel(selectedProduct);

            if (!ViewModel.ShoppingCart.Any(s => s.Product == selectedProduct))
            {
                ViewModel.ShoppingCart.Add(shoppingCartItem);
            }
            else
            {
                ViewModel.ShoppingCart.First(s => s.Product == selectedProduct).IncreaseCount();
            }

            asbProductSearch.Text = null;
        }

        private void DecreaseProduct(object sender, MouseButtonEventArgs e)
        {
            var item = dataGrid.SelectedItem as ShoppingCartViewModel;

            if (item == null) return;

            item.DecreaseCount();
        }

        private void IncreaseProduct(object sender, MouseButtonEventArgs e)
        {
            var item = dataGrid.SelectedItem as ShoppingCartViewModel;

            if (item == null) return;

            item.IncreaseCount();
        }

        private void RemoveProductFromCart(object sender, RoutedEventArgs e)
        {
            var item = dataGrid.SelectedItem as ShoppingCartViewModel;

            if (item == null) return;

            ViewModel.ShoppingCart.Remove(item);
        }

        #region Client autosuggestbox

        private void asbClientSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchClient(sender.Text);

                if (suggestions.Count > 0)
                    sender.ItemsSource = suggestions;
                else
                    sender.ItemsSource = new string[] { "Müşteri bulunamadı" };
            }
        }

        private void asbClientSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (ViewModel.Client != args.SelectedItem as ClientModel)
                ViewModel.Client = args.SelectedItem as ClientModel;
        }

        private readonly List<ClientModel> clients = CommonService.GetAll<ClientModel>(DbTables.Clients).ToList();
        private List<ClientModel> SearchClient(string query)
        {
            var querySplit = query.Split(' ');
            var suggestions = clients.Where(
                item =>
                {
                    bool flag = true;
                    foreach (string queryToken in querySplit)
                    {
                        if (item.FullName.IndexOf(queryToken, StringComparison.CurrentCultureIgnoreCase) < 0)
                        {
                            flag = false;
                        }
                    }
                    return flag;
                });
            return suggestions.OrderByDescending(i => i.FullName.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ThenBy(i => i.FullName).ToList();
        }

        #endregion
    }
}
