namespace Hospital.Train.PL.ViewModels.AccountController
{
    public class SendResetPassword
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
