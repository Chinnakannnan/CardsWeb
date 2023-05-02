namespace PPICards.Models
{
    public class FetchBalanceModel
    {
        public List<FetchBalanceResult> result { get; set; }
        public FetchBalanceException exception { get; set; }
        public string pagination { get; set; }
    }
    public class FetchBalanceResult
    {
        public string entityId { get; set; }
        public string productId { get; set; }
        public decimal balance { get; set; }
        public string yseId { get; set; }

    }

    public class FetchBalanceException
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        //public string fieldErrors { get; set; }
        //public IList<string> fieldErrors { get; set; }
        public string[] fieldErrors { get; set; }
        public string message { get; set; }

        public string localizedMessage { get; set; }
        //public IList<string> suppressed { get; set; }
        public string[] suppressed { get; set; }
        //suppressed
    }
}
