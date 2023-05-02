namespace PPICards.Models
{
    public class ResetPassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CustomerId { get; set; }

    }
}
