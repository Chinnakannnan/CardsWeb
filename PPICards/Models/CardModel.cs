namespace PPICards.Models
{
    public class CardModel
    {
        public string CardNumber { get; set; } = string.Empty;
        public string CardExpiryDate { get; set; } = string.Empty;
        public string KitNo { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public DateTime? dob { get; set; }
    }

    public class CardRequest
    {
        public string entityId { get; set; }
    }

    public class CardResponseResult
    {
        public List<string> cardList { get; set; }
        public List<string> kitList { get; set; }
        public List<string> expiryDateList { get; set; }
        public List<string> cardStatusList { get; set; }
        public List<string> cardTypeList { get; set; }
    }

    public class CardResponse
    {
        public CardResponseResult result { get; set; }
    }

    public class CVVRequest
    {
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string expiryDate { get; set; }
        public string dob { get; set; }
    }

    public class PinRequest
    {
        public string kitNo { get; set; }
        public string entityId { get; set; }
        public string dob { get; set; }
    }

    public class CvvDetails
    {
        public string cvv { get; set; }
    }

    public class CVVResponse
    {
        public CvvDetails result { get; set; }

    }

    public class CardCVVModelResponse
    {
        public CardCVVModelRequest result { get; set; }
        public CardModelException exception { get; set; }
        public string pagination { get; set; }
    }
    public class CardCVVModelRequest
    {
        public string cvv { get; set; }
    }

    public class CardModelException
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        public string[] fieldErrors { get; set; }
        //suppressed
    }

    public class PinResponse
    {
        public string result { get; set; }

        public CardModelException exception { get; set; }

        public string pagination { get; set; }
    }

}
