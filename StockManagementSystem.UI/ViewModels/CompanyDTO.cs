using StockManagementSystem.Library;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockManagementSystem.UI.ViewModels
{
    public class CompanyDTO : INotifyPropertyChanged
    {
        private CompanyModel _company;

        public event PropertyChangedEventHandler PropertyChanged;

        public CompanyModel Company
        {
            get { return _company; }
            set
            {
                _company = value;
                UpdateCompanyPayments();
            }
        }

        public ObservableCollection<CompanyPaymentModel> CompanyPayments { get; private set; }

        public decimal CompanyDebtAmount
        {
            get
            {
                if (CompanyPayments != null)
                    return CompanyPayments.Where(s => s.Type == 0).Sum(s => s.Amount);

                return 0;
            }
        }

        public decimal CompanyPaidAmount
        {
            get
            {
                if (CompanyPayments != null)
                    return CompanyPayments.Where(s => s.Type == 1).Sum(s => s.Amount);

                return 0;
            }
        }

        public decimal CompanyUnpaidAmount
        {
            get
            {
                return CompanyDebtAmount - CompanyPaidAmount;
            }
        }

        public decimal CurrentMonthDebtAmount
        {
            get
            {
                if (CompanyPayments != null)
                    return CompanyPayments.Where(s => s.TransactionDate.Month == DateTime.Now.Month && s.TransactionDate.Year == DateTime.Now.Year && s.Type == 0)
                .Sum(s => s.Amount);

                return 0;
            }
        }

        public decimal CurrentMonthPaidAmount
        {
            get
            {
                if (CompanyPayments != null)
                    return CompanyPayments.Where(s => s.TransactionDate.Month == DateTime.Now.Month && s.TransactionDate.Year == DateTime.Now.Year && s.Type == 1)
                .Sum(s => s.Amount);

                return 0;
            }
        }

        public decimal NextMonthDebtAmount
        {
            get
            {
                if (CompanyPayments != null)
                {
                    var currDate = (DateTime.Now).AddMonths(1);

                    return CompanyPayments.Where(s => s.TransactionDate.Month == currDate.Month && s.TransactionDate.Year == currDate.Year)
                        .Sum(s => s.Amount);
                }

                return 0;
            }
        }

        public decimal NextMonthPaidAmount
        {
            get
            {
                if (CompanyPayments != null)
                {
                    var currDate = (DateTime.Now).AddMonths(1);

                    return CompanyPayments.Where(s => s.TransactionDate.Month == currDate.Month && s.TransactionDate.Year == currDate.Year && s.Type == 1)
                        .Sum(s => s.Amount);
                }

                return 0;
            }
        }

        public CompanyDTO(CompanyModel model)
        {
            Company = model;
        }

        private void UpdateCompanyPayments()
        {
            var payments = CompanyPaymentService.GetCompanyPaymentsByCompany(Company);

            if (CompanyPayments == null)
            {
                CompanyPayments = new ObservableCollection<CompanyPaymentModel>();
                CompanyPayments.CollectionChanged += delegate { OnPropertyChanged(); };
            }

            foreach (var item in payments)
                CompanyPayments.Add(item);
        }

        public void InsertLastRecord()
        {
            CompanyPayments.Add(CommonService.GetLastInsertedRow<CompanyPaymentModel>(DbTables.CompanyPayments).First());
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
