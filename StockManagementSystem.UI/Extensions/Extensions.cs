using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem
{
    public static class Extensions
    {
        public static string ConvertToCurrencyString(this decimal value)
        {
            CultureInfo tr = new CultureInfo("tr-TR");
            tr = (CultureInfo)tr.Clone();
            tr.NumberFormat.CurrencyPositivePattern = 3;
            tr.NumberFormat.CurrencyNegativePattern = 3;

            return value.ToString("C2", tr);
        }

        public static string GetMonthString(this DateTime value)
        {
            return value.ToString("MMMM", CultureInfo.CreateSpecificCulture("tr-TR"));
        }
    }
}
