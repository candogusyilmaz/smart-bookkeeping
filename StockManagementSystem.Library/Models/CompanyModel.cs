namespace StockManagementSystem.Library
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }

        public decimal Total => CompanyPaymentService.GetCompanyTotalDebt(this);
        public decimal Paid => CompanyPaymentService.GetCompanyPaid(this);
        public decimal Unpaid => Total - Paid;

    }
}
