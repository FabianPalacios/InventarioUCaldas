using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LogicaNegocio.Services
{
    public class Notifications
    {
        public Boolean SendEmail(string subject,
                                 string content,
                                 string toName,
                                 string toEmail)
        {
            var client = new SendGridClient("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");
            string emailFrom = ConfigurationManager.AppSettings["EmailFromSendGrid"];
            string nameFrom = ConfigurationManager.AppSettings["NameFromSendGrid"];
            var from = new EmailAddress(emailFrom, nameFrom);
            var to = new EmailAddress(toEmail, toName);
            var plainTextContent = content;
            var htmlContent = content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
            Console.WriteLine(response.Status);
            return true;
        }
    }
}
