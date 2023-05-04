namespace PPICards.Models
{
    public class AddKitCardDetails
    {
        public string KitReferenceNumber { get; set; }
        public string CardNumber { get; set; }
        public DateTime? CardExpiryDate { get; set; }
        //public string CardtypeLabel { get; set; }
        public string Cardtype { get; set; }

    }

    public class AddbulkKitCardDetails
    {
        public string KitReferenceNumber { get; set; }
        public string CardNumber { get; set; }
        public DateTime? CardExpiryDate { get; set; }
        //public string CardtypeLabel { get; set; }
        public string Cardtype { get; set; }

    }

    public class StatusResponseModel
    {
        public string statuscode { get; set; }
        public string statusdesc { get; set; }
        public string entityId { get; set; }

    }
    public class AddKitCardDetails_staus
    {

    }
}
