using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace backuperApi
{
    public class Mail
    {
        private Database database = new Database();

        public void SendMail(string subject, string message, int userId)
        {
            string mailTo = this.database.Users.Where(u => u.Id == userId).First().Email;

            MailMessage mail = new MailMessage("mmmproject@gmail.com", mailTo);
            SmtpClient client = new SmtpClient()
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
            };

            mail.Subject = subject;
            mail.Body = message;
            client.Send(mail);
        }
    }
}