using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = "admin@com.com";
        private string _mailFrom = "noreply@com.com";

        public void Send(string subject, string message)
        {
            //not real sending
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with CloudMailServie");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message {message}");
        }
    }
}
