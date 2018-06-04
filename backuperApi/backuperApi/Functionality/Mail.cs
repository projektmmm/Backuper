using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;

namespace backuperApi
{
    public class Mail
    {
        private Database database = new Database();

        public void SendMail(string subject, string message, string mailTo)
        {
            //MailMessage mail = new MailMessage("mmmproject@gmail.com", mailTo);
            //SmtpClient client = new SmtpClient()
            //{
            //    Port = 25,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Host = "smtp.gmail.com",
            //};

            //mail.Subject = subject;
            //mail.Body = message;
            //mail.Attachments.Add(new Attachment(@"C:\Settings\DaemonSettings.txt"));
            //client.Send(mail);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("mmmprojectmmm@gmail.com");
            mail.To.Add(mailTo);
            mail.Subject = subject;
            mail.Body = message;

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("your attachment file");
            //mail.Attachments.Add(attachment);
            mail.Attachments.Add(new Attachment(@"C:\Settings\DaemonSettings.txt"));

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("mmmprojectmmm@gmail.com", "Ab123456bA");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}