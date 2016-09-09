using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace SecondIINoneMC.Core.Helpers
{
    public static class EmailHelper
    {
        public static bool SendResetPasswordEmail(string username, string newPassword)
        {
            var imagePath = String.Format(@"{0}Email\email_header.jpg", AppDomain.CurrentDomain.BaseDirectory);
            var templateFilePath = String.Format(@"{0}email\EMailTemplate.htm", AppDomain.CurrentDomain.BaseDirectory);
            
            var sb = new StringBuilder();
            sb.Append("<p>Please return to the site and log in using the following information.</p>");
            sb.Append("<p>Username: " + username + "</p>");
            sb.Append("<p>Password: " + newPassword + "</p>");

            // new up the e-mail object and set the properties.
            var mailMessage = new MailMessage("info@secondIInonemc.com", username);
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = String.Format("Your password has been reset.");
            mailMessage.Attachments.Add(new Attachment(imagePath));

            // Merge it with the template...
            var email = new HTMLEmail(templateFilePath);
            email.TeaserMessage = "Your password has been reset.";
            email.Title = String.Format("Your password has been reset.");
            email.Body = sb.ToString();

            mailMessage.Body = email.GetHtml();

            // send the message
            var isMessageSent = SendMessage(mailMessage);

            return isMessageSent;
        }

        public static bool SendEmail(String fromEmailAddress, 
            String toEmailAddress, 
            String subject, 
            String messageBody,
            String ccEmailAddress = "", 
            String bccEmailAddress = "", 
            Boolean isHtml = true)
        {
            String smtpServer = ConfigurationManager.AppSettings["SmtpServer"].ToString();

            var message = new MailMessage();

            message.IsBodyHtml = isHtml;
            message.From = new MailAddress("info@secondIInonemc.com");
            message.To.Add(toEmailAddress);

            if (ccEmailAddress.Length > 0)
            {
                message.CC.Add(ccEmailAddress);
            }

            if (bccEmailAddress.Length > 0)
            {
                message.Bcc.Add(bccEmailAddress);
            }

            message.Subject = subject;
            message.Body = messageBody;

            //if(attachments.Length > 0)
            //{
            //    for(Int32 x = 0; x++; x < attachments.Length)
            //    {
            //        message.Attachments.Add(new Attachment(        
            //    }
            //}

            // send the message
            var isMessageSent = SendMessage(message);

            return isMessageSent;
        }

        private static bool SendMessage(MailMessage mailMessage)
        {
            bool isMessageSent = false;

            try
            {
                var smtpServer = ConfigurationManager.AppSettings["SmtpServer"].ToString();

                var smtpClient = new SmtpClient();

                smtpClient.Host = smtpServer;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("info@secondIInonemc.com", "comegetsome");

                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                isMessageSent = false;
            }

            return isMessageSent;
        }
    }
}
