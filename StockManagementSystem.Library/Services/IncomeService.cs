using System.Collections.Generic;

namespace StockManagementSystem.Library
{
    public static class IncomeService
    {
        public static void InsertIncome(IncomeModel model)
        {
            string sql = "insert into Incomes (Note, Amount, Type, Date) " +
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

        public static List<IncomeModel> GetAll()
        {
            string sql = "select * from Incomes";

            return SqliteDataAccess.LoadData<IncomeModel>(sql, null);
        }
    }
}
