using System;

namespace StockManagementSystem.Library
{
    public class IncomeModel
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
