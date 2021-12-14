
﻿using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Services
{
    public class Notifications
    {


        public Boolean SendEmail(string subject, string content, string toName, string toEmail)
        {
            //string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient("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");
            string emailFrom = System.Configuration.ConfigurationSettings.AppSettings["EmailFromSendGrid"];
            string nameFrom = System.Configuration.ConfigurationSettings.AppSettings["NameFromSendGrid"];
            var from = new EmailAddress(emailFrom, nameFrom);
            var to = new EmailAddress(toEmail, toName);
            var plainTextContent = content;
            var htmlContent = content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);

            return true;
        }

    }
}
