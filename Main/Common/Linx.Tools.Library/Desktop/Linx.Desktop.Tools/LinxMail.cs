using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Tools
{
    public class LinxMail
    {
        public static void Send(string toAddress, string subject, bool isBodyHtml, string body)
        {
            try
            {
                Hashtable config = System.Configuration.ConfigurationManager.GetSection("SendEmailSettings") as Hashtable;

                if (config.IsNullOrEmpty())
                    throw new DomainException("Configuração para envio de email não foi encontrada.".Translate());

                string sender = config["sender"].ToString();
                string smtpServer = config["smtpServer"].ToString();
                int smtpPort = Convert.ToInt16(config["port"].ToString().IsNullOrEmpty() ? 0 : config["port"]);
                int timeout = Convert.ToInt16(config["serverTimeout"].ToString().IsNullOrEmpty() ? 10000 : config["serverTimeout"]);
                string user = config["user"].ToString();
                string password = config["password"].ToString();
                bool enableSsl = Convert.ToBoolean(config["enableSsl"].ToString().IsNullOrEmpty() ? "false" : config["enableSsl"]);

                if (sender.IsNullOrEmpty() || smtpServer.IsNullOrEmpty() || smtpPort.IsNullOrEmpty())
                    throw new DomainException("Configuração para envio de email está inconsistente.".Translate());

                Send(sender, toAddress, subject, isBodyHtml, body, smtpServer, smtpPort, timeout, user, password, enableSsl);
            }
            catch (Exception oException)
            {
                throw new DomainException("Erro no envio automático de email.\n\n" + oException.Message);
            }
        }

        public static void Send(string sender, string toAddress, string subject, bool isBodyHtml, string body, string smtpServer, int smtpPort, int timeout, string user, string password, bool enableSsl)
        {
            MailMessage newMail = new MailMessage();
            newMail.From = new MailAddress(sender);
            newMail.To.Add(toAddress);
            newMail.Subject = subject;
            newMail.SubjectEncoding = Encoding.UTF8;
            newMail.IsBodyHtml = isBodyHtml;
            newMail.Body = body;
            newMail.BodyEncoding = Encoding.UTF8;
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Timeout = timeout;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(user, password);
            smtpClient.EnableSsl = enableSsl;
            smtpClient.Send(newMail);
        }
    }
}
