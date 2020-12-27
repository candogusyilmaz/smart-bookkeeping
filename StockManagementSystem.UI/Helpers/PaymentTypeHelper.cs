namespace StockManagementSystem.UI
{
    public static class PaymentTypeHelper
    {
        public static string Cash
        {
            get
            {
                return "Nakit";
            }
        }

        public static string CreditCard
        {
            get
            {
                return "Kredi Kartı";
            }
        }

        public static string Debt
        {
            get
            {
                return "Borç";
            }
        }

        public static string Payment
        {
            get
            {
                return "Ödeme";
            }
        }

        public static string DelayedPayment
        {
            get
            {
                return "Veresiye";
            }
        }
    }
}
