namespace PPICards.Models
{
    public class WalletModel
    {

        public string customerId { get; set; }
        public string cardReferenceId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string trnType { get; set; }
        public string fromEntityId { get; set; }

    }
    public class LockUnlock
    {
        public string customerId { get; set; }
        public string cardReferenceId { get; set; }
        public string entityId { get; set; }
        public string reason { get; set; }
        public string flag { get; set; }
        public string emailID { get; set; }

    }

    public class GetLimit
    {
        public string ATM { get; set; }
        public string CONTACTLESS { get; set; }
        public string POS { get; set; }
        public string ECOM { get; set; }       



    }
    public class GetLimitRequest
    {
        public string KitReferenceNumber { get; set; } 
 

    }
}
