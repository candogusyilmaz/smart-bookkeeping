using System;

namespace StockManagementSystem.Library
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }

        private ClientModel _client;
        public ClientModel Client
        {
            get
            {
                if (_client == null)
                    _client = ClientService.Get(ClientId);
                return _client;
            }
        }
    }
}
