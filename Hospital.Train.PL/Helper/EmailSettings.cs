using Hospital.Train.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Hospital.Train.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("abdalllahsanadabo@gmail.com","05452258445");
            client.Send("abdalllahsanadabo@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
