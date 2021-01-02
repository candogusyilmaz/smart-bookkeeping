using System;

namespace StockManagementSystem.Library
{
    public class CompanyPaymentModel
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public byte[] Attachment { get; set; }

        private CompanyModel _company;
        public CompanyModel Company
        {
            get
            {
                if (_company == null)
                    _company = CompanyService.GetCompany(CompanyId);
                return _company;
            }
        }
    }
}
