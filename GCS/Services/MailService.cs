using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace GCS.Services {
    public class MailService {
        public void Send(MailMessage message) {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;

            try {
                client.PickupDirectoryLocation = "c:\\Inetpub\\mailroot\\Pickup\\";
                client.Send(message);
            } catch {

            }
        }

        public MailMessage CreateQuotaLimitMessage(string URL, string domain) {
            MailMessage message = new MailMessage() {
                From = new MailAddress("noreply@w3s.nl"),
                Subject = "Umbaco GCS - Quota limit excedeed",
                IsBodyHtml = true,
                Body = "Daily Google Custom Search limit has been excedeed.<br/><br/> <b>Domain:</b>" + domain + "<br/><br/><b>Last requested URL:</b>" + URL
            };

            message.To.Add(new MailAddress("development@w3s.nl"));

            return message;
        }

        public MailMessage SendErrorMail(string URL, Exception ex) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<b>Error TargetSite:</b> " + ex.TargetSite + "<br/>");
            sb.AppendLine("<b>Error Message:</b> " + ex.Message + "<br/>");
            sb.AppendLine("<b>Error StackTrace:</b> " + ex.StackTrace + "<br/>");
            sb.AppendLine("<b>Error InnerException:</b> " + ex.InnerException + "<br/>");
            sb.AppendLine("<b>Error HResult:</b> " + ex.HResult + "<br/>");
            sb.AppendLine("<b>Error Source:</b> " + ex.Source + "<br/>");
            sb.AppendLine("<b>Error Source:</b> " + ex.HelpLink + "<br/>");

            MailMessage message = new MailMessage() {
                From = new MailAddress("noreply@w3s.nl"),
                Subject = "Umbaco GCS - Error",
                IsBodyHtml = true,
                Body = "Error at URL: " + URL + ".<br/><br/>" + "Data:<br/></br/>" + sb.ToString()
            };

            message.To.Add(new MailAddress("development@w3s.nl"));

            return message;
        }
    }
}