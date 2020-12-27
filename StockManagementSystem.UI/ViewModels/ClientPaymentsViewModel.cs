using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StockManagementSystem.UI
{
    public class ClientPaymentsViewModel : BaseViewModel
    {
        public ClientModel Client { get; set; }

        public ClientPaymentModel SelectedClientPayment { get; set; }

        public List<ClientPaymentModel> ClientPayments { get; private set; }

        public BindingList<ClientPaymentModel> FilteredClientPayments { get; set; }

        public string MonthString { get; set; }
        public decimal MonthTotalAmount { get; set; }
        public decimal MonthCreditCardAmount { get; set; }
        public decimal MonthCashAmount { get; set; }
        public decimal MonthOpenAccountAmount { get; set; }

        public ClientPaymentsViewModel(ClientModel model)
        {
            Client = model;
            ClientPayments = CommonService.GetAll<ClientPaymentModel>(DbTables.ClientPayments).ToList();
            FilteredClientPayments = new BindingList<ClientPaymentModel>(ClientPayments);

            ChangeMonthDetails(DateTime.Now);
        }

        public void ChangeMonthDetails(DateTime date)
        {
            var monthData = FilteredClientPayments.Where(s => s.Date.Month == date.Month && s.Date.Year == date.Year);

            MonthString = $"{date.GetMonthString()}, {date.Year}: ";

            MonthTotalAmount = monthData.Where(s => s.Type != 0).Sum(s => s.Amount);

            MonthCreditCardAmount = monthData.Where(s => s.Type == 1).Sum(s => s.Amount);

            MonthCashAmount = monthData.Where(s => s.Type == 2).Sum(s => s.Amount);

            MonthOpenAccountAmount = monthData.Where(s => s.Type == 0).Sum(s => s.Amount);
        }

        public void InsertLastRecords(int count = 1)
        {
            foreach (var item in CommonService.GetLastInsertedRow<ClientPaymentModel>(DbTables.ClientPayments, count))
            {
                ClientPayments.Add(item);
            }

            FilteredClientPayments = new BindingList<ClientPaymentModel>(ClientPayments);
        }
    }
}
