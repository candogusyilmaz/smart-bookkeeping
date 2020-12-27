using System.Collections.Generic;

namespace StockManagementSystem.Library
{
    public static class ExpenseService
    {
        public static void InsertExpense(ExpenseModel model)
        {
            string sql = "insert into Expenses (Note, Amount, Type, Date) " +
                         "values (@Note, @Amount, @Type, @Date)";

            var parameters = new Dictionary<string, object>
            {
                { "@Note", model.Note},
                { "@Amount", model.Amount},
                { "@Type", model.Type},
                { "@Date", model.Date}
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }

        public static List<ExpenseModel> GetAll()
        {
            string sql = "select * from Expenses";

            return SqliteDataAccess.LoadData<ExpenseModel>(sql, null);
        }
    }
}
