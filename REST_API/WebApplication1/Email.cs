using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;


namespace WebApplication1
{
     class Email
    {
       
        public  void SendMail(string ToEmail,string Subject,string Body)

        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mmmprojectmmm@gmail.com");
                mail.To.Add(ToEmail);
                mail.Subject = Subject;
                mail.Body = Body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("mmmprojectmmm", "Ab123456bA");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //return report o succesu 
            }
            catch (Exception ex)
            {
                //return report o erroru 
            }
        }
    }
}