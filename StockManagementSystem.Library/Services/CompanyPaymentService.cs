using System.Collections.Generic;

namespace StockManagementSystem.Library
{
    public static class CompanyPaymentService
    {
        public static decimal GetCompanyTotalDebt(CompanyModel model)
        {
            string sql = $"select sum(Amount) from CompanyPayments where CompanyId={model.Id} and Type=0";

            return SqliteDataAccess.LoadSingleData<decimal>(sql, null);
        }

        public static decimal GetCompanyPaid(CompanyModel model)
        {
            string sql = $"select sum(Amount) from CompanyPayments where CompanyId={model.Id} and Type=1";

            return SqliteDataAccess.LoadSingleData<decimal>(sql, null);
        }

        public static List<CompanyPaymentModel> GetCompanyPayments(CompanyModel model)
        {
            string sql = "select * from CompanyPayments where CompanyId=" + model.Id;

            return SqliteDataAccess.LoadData<CompanyPaymentModel>(sql, null);
        }

        public static void InsertCompanyPayment(CompanyPaymentModel model)
        {
            string sql = "insert into CompanyPayments (CompanyId, Type, Amount, TransactionDate, Note, Attachment) " +
                         "values (@CompanyId, @Type, @Amount, @TransactionDate, @Note, @Attachment)";

            var parameters = new Dictionary<string, object>
            {
                { "@CompanyId", model.CompanyId},
                { "@Type", model.Type},
                { "@Amount", model.Amount},
                { "@TransactionDate", model.TransactionDate},
                { "@Note", model.Note},
                { "@Attachment", model.Attachment}
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }

        public static void DeleteCompanyPayment(int id)
        {
            string sql = "delete from CompanyPayments where Id=" + id;

            SqliteDataAccess.SaveData(sql, null);
        }

        public static void DeleteCompanyPayment(CompanyPaymentModel model)
        {
            string sql = "delete from CompanyPayments where Id=" + model.Id;

            SqliteDataAccess.SaveData(sql, null);
        }
    }
}
