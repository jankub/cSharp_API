using System.Diagnostics;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "admin@com.com";
        private string _mailFrom = "noreply@com.com";

        public void Send(string subject, string message)
        {
            //not real sending
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailServie");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message {message}");
        }
    }

}
