using StockManagementSystem.UI.ViewModels;
using System.Windows.Controls;

namespace StockManagementSystem.UI.Views
{
    public partial class SalesView : UserControl
    {
        private SalesListView salesListView;
        private SaleDetailsView saleDetailsView;

        public SalesView()
        {
            InitializeComponent();

            ChangeContent(GetSalesListView());
        }
        private void ChangeContent(object view)
        {
            if (view != null)
                content.Content = view;
        }

        private SalesListView GetSalesListView()
        {
            salesListView = new SalesListView();
            salesListView.SaleDetails.Click += delegate { ChangeContent(GetSaleDetailsView()); };

            return salesListView;
        }

        private SaleDetailsView GetSaleDetailsView()
        {
            if (salesListView.dataGrid.SelectedItem == null)
                return null;

            saleDetailsView = new SaleDetailsView(((SaleDTO)salesListView.dataGrid.SelectedItem).Sale);
            saleDetailsView.BackToSalesList.Click += delegate { ChangeContent(GetSalesListView()); };

            return saleDetailsView;
        }
    }
}
