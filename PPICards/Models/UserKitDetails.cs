namespace PPICards.Models
{
    public class UserKitDetails
    {
        public string KitMappingId { get; set; }
        public string KitReferenceNumber { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string EntityId { get; set; }
        public string CardStatus { get; set; }

        //public int kitMappingId { get; set; }
        //public string kitReferenceNumber { get; set; }
        //public string cardType { get; set; }
        //public string cardCategory { get; set; }
        //public string entityId { get; set; }
        //public string CardStatus { get; set; }
        //public string CardNo { get; set; }
        //public string customerId { get; set; }
        //public string productId { get; set; }
        public DateTime DOB { get; set; }
        //public DateTime createdDate { get; set; }
        //public string createdBy { get; set; }
        //public DateTime modifiedDate { get; set; }
        //public string modifiedBy { get; set; }
        //public bool isActive { get; set; }
        //public int nonKyc_Dom_ATM_Limit { get; set; }
        //public int nonKyc_Dom_tab_Limit { get; set; }
        //public int nonKyc_Dom_Online_Limit { get; set; }
        //public int nonKyc_Dom_Outlet_Limit { get; set; }
        //public int kyc_Dom_ATM_Limit { get; set; }
        //public int kyc_Dom_tab_Limit { get; set; }
        //public int kyc_Dom_Online_Limit { get; set; }
        //public int kyc_Dom_Outlet_Limit { get; set; }

    }

    public class ListUserKitDetails
    {
        public List<UserKitDetails> UserKitDetails { get; set; } = new List<UserKitDetails>();
        public CardModel CardModel { get; set; } = new CardModel();
    }

    public class KITMap
    {
        public string customerId { get; set; }
    }

    public class ActivateCardDetail
    {
        public string kitReferenceNumber { get; set; }
        public string entityId { get; set; }
        public string customerId { get; set; }
        public string mobileNumber { get; set; }
        public string otp { get; set; }
    }
}
