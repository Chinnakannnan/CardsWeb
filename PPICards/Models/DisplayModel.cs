namespace PPICards.Models
{
    public class DisplayModel
    {
        public bool IsSuccess { get; set; } = false;

        public string ErrorMessage { get; set; } = OnboardConstants.ErrorMessage;
    }
}
