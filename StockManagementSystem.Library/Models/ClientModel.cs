namespace StockManagementSystem.Library
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public byte[] Attachment { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
