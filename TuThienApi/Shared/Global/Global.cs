using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TuThienApi.Shared.Global
{
    public class Global
    {
        public static bool SendEmail(string sendto, string subject, string content,
            string from, string pass, string addresshost, int mailport, bool statusSsl)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(addresshost);

                mail.From = new MailAddress(from);
                mail.To.Add(sendto);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = content;
                
                mail.Priority = MailPriority.High;

                SmtpServer.Port = mailport;
                SmtpServer.Credentials = new NetworkCredential(from, pass);
                SmtpServer.EnableSsl = statusSsl;

                SmtpServer.Send(mail);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
