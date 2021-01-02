using System;

namespace StockManagementSystem.Library
{
    public class ClientPaymentModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
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
