using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StockManagementSystem.UI.Converters
{
    public class ClientPaymentTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int temp = (int)value;

            if(temp == 1)
            {
                return PaymentTypeHelper.CreditCard;
            }
            else if(temp == 2)
            {
                return PaymentTypeHelper.Cash;
            }
            else
            {
                return PaymentTypeHelper.DelayedPayment;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
