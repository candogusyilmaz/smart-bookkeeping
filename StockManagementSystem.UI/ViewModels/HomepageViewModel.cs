using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StockManagementSystem.UI.ViewModels
{
    public class HomepageViewModel : BaseViewModel
    {
        public decimal NetWorthToday { get; set; }
        public decimal NetWorthMonth { get; set; }

        public ObservableCollection<ClientPaymentModel> UpcomingOpenAccounts { get; set; }

        public HomepageViewModel()
        {
            UpcomingOpenAccounts = new ObservableCollection<ClientPaymentModel>(CommonService.GetAll<ClientPaymentModel>(DbTables.ClientPayments).Where(s => s.Type == 0).OrderBy(s => s.Date));
            CalculateNetWorthToday();
            CalculateNetWorthMonth();
        }

        private void CalculateNetWorthToday()
        {
            var dateToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var salesToday = CommonService.GetAll<SaleModel>(DbTables.Sales).Where(s => s.TransactionDate.Day == dateToday.Day &&
                                                                                        s.TransactionDate.Month == dateToday.Month &&
                                                                                        s.TransactionDate.Year == dateToday.Year);

            List<BillModel> billsToday = new List<BillModel>();

            foreach (var item in salesToday)
            {
                billsToday.AddRange(BillService.GetAll(item));
            }

            decimal productTotalAmount = 0;

            foreach (var item in billsToday)
            {
                productTotalAmount += ProductService.Get(item.ProductId).UnitBuyPrice * item.ProductAmount;
            }


            var clientTotal = CommonService.GetAll<ClientPaymentModel>(DbTables.ClientPayments).Where(s => s.Date.Day == dateToday.Day &&
                                                                                        s.Date.Month == dateToday.Month &&
                                                                                        s.Date.Year == dateToday.Year && s.Type != 0).Sum(s => s.Amount);

            NetWorthToday = clientTotal - productTotalAmount;
        }
        private void CalculateNetWorthMonth()
        {
            var dateToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var salesToday = CommonService.GetAll<SaleModel>(DbTables.Sales).Where(s =>
                                                                                        s.TransactionDate.Month == dateToday.Month &&
                                                                                        s.TransactionDate.Year == dateToday.Year);

            List<BillModel> billsToday = new List<BillModel>();

            foreach (var item in salesToday)
            {
                billsToday.AddRange(BillService.GetAll(item));
            }

            decimal productTotalAmount = 0;

            foreach (var item in billsToday)
            {
                productTotalAmount += ProductService.Get(item.ProductId).UnitBuyPrice * item.ProductAmount;
            }


            var clientTotal = CommonService.GetAll<ClientPaymentModel>(DbTables.ClientPayments).Where(s =>
                                                                                        s.Date.Month == dateToday.Month &&
                                                                                        s.Date.Year == dateToday.Year && s.Type != 0).Sum(s => s.Amount);

            NetWorthMonth = clientTotal - productTotalAmount;
        }
    }
}
