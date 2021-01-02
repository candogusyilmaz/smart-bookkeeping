using StockManagementSystem.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StockManagementSystem.UI
{
    public class CompanyDebtViewModel : BaseViewModel
    {
        public bool IsSearchPaneOpen { get; set; }

        #region Filter Properties


        private DateTime _dateHighFilter;
        public DateTime DateHighFilter
        {
            get { return _dateHighFilter; }
            set
            {
                _dateHighFilter = value.AddSeconds(86399);
                FilterCompanyPayments();
            }
        }

        private DateTime _dateLowFilter;
        public DateTime DateLowFilter
        {
            get { return _dateLowFilter; }
            set
            {
                _dateLowFilter = value;
                FilterCompanyPayments();
            }
        }

        private decimal _priceLowFilter;
        public decimal PriceLowFilter
        {
            get { return _priceLowFilter; }
            set
            {
                _priceLowFilter = value;
                FilterCompanyPayments();
            }
        }

        private decimal _priceHighFilter;
        public decimal PriceHighFilter
        {
            get { return _priceHighFilter; }
            set
            {
                _priceHighFilter = value;
                FilterCompanyPayments();
            }
        }

        private string _descriptionFilter;
        public string DescriptionFilter
        {
            get { return _descriptionFilter; }
            set
            {
                _descriptionFilter = value;
                FilterCompanyPayments();
            }
        }

        #endregion


        private readonly List<CompanyPaymentModel> CompanyPayments;
        public CompanyModel Company { get; set; }
        public BindingList<CompanyPaymentModel> FilteredCompanyPayments { get; set; }


        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal UnpaidAmount { get; set; }
        public string MonthString { get; set; }
        public decimal MonthTotalAmount { get; set; }
        public decimal MonthPaidAmount { get; set; }
        public string NextMonthString { get; set; }
        public decimal NextMonthTotalAmount { get; set; }
        public decimal NextMonthPaidAmount { get; set; }

        public DateTime DisplayDateStart { get; set; }
        public DateTime DisplayDateEnd { get; set; }

        public CompanyDebtViewModel(CompanyModel model)
        {
            Company = model;

            CompanyPayments = CompanyPaymentService.GetCompanyPayments(Company);

            if (CompanyPayments.Any())
            {
                DisplayDateStart = CompanyPayments.OrderBy(s => s.TransactionDate).First().TransactionDate;
                DisplayDateEnd = CompanyPayments.OrderByDescending(s => s.TransactionDate).First().TransactionDate;
            }

            FilteredCompanyPayments = new BindingList<CompanyPaymentModel>();

            foreach (var item in CompanyPayments)
                FilteredCompanyPayments.Add(item);

            CalculateAmounts();
        }

        private void CalculateAmounts()
        {
            if (FilteredCompanyPayments.Count < 1) return;

            TotalAmount = FilteredCompanyPayments.Where(s => s.Type == 0).Sum(s => s.Amount);
            PaidAmount = FilteredCompanyPayments.Where(s => s.Type == 1).Sum(s => s.Amount);
            UnpaidAmount = TotalAmount - PaidAmount;

            MonthTotalAmount = FilteredCompanyPayments.Where(s => s.TransactionDate.Month == DateTime.Now.Month && s.TransactionDate.Year == DateTime.Now.Year && s.Type == 0)
                .Sum(s => s.Amount);
            MonthPaidAmount = FilteredCompanyPayments.Where(s => s.TransactionDate.Month == DateTime.Now.Month && s.TransactionDate.Year == DateTime.Now.Year && s.Type == 1)
                .Sum(s => s.Amount);

            var nextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);

            NextMonthTotalAmount = FilteredCompanyPayments.Where(s => s.TransactionDate.Month == nextMonth.Month && s.TransactionDate.Year == nextMonth.Year & s.Type == 0)
                        .Sum(s => s.Amount);
            NextMonthPaidAmount = FilteredCompanyPayments.Where(s => s.TransactionDate.Month == nextMonth.Month && s.TransactionDate.Year == nextMonth.Year && s.Type == 1)
                        .Sum(s => s.Amount);

            MonthString = DateTime.Now.GetMonthString();
            NextMonthString = nextMonth.GetMonthString();
        }

        private void FilterCompanyPayments()
        {
            FilteredCompanyPayments.Clear();

            var payments = CompanyPayments.Where(s => s.Note.Contains(DescriptionFilter));


            if (PriceLowFilter <= PriceHighFilter && PriceHighFilter + PriceLowFilter > 1)
            {
                payments = payments.Where(s => PriceLowFilter <= s.Amount && s.Amount <= PriceHighFilter);
            }

            if (DateLowFilter <= DateHighFilter)
            {
                payments = payments.Where(s => DateLowFilter <= s.TransactionDate && s.TransactionDate <= DateHighFilter);
            }

            foreach (var item in payments)
                FilteredCompanyPayments.Add(item);

            CalculateAmounts();
        }

        public void InsertLastRecord()
        {
            var item = CommonService.GetLastInsertedRow<CompanyPaymentModel>(DbTables.CompanyPayments).First();

            CompanyPayments.Add(item);
            FilteredCompanyPayments.Add(item);
        }
    }
}
