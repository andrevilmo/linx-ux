using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Business.Common
{
    public class Email : MailMessage
    {
        private SmtpClient _SmtpClient = null;
        private string _SmtpHost;
        private int? _SmtpPort = 25; //587
        private string _SmtpUserName;
        private string _SmtpPassword;
        private bool _SmtpUseCredentials;
        private bool _SmtpEnableSSL = false;

        public delegate void EnvioEmailCompleto(object param);
        public event EnvioEmailCompleto EmailEnviado;

        private void CallEmailEnviado(object param)
        {
            if (EmailEnviado != null)
                EmailEnviado(param);
        }

        public Email(string smtpHost, bool smtpUseCredentials, string smtpUserName = null, string smtpPassword = null, int? smtpPort = 25, bool smtpEnableSSL = false)
        {
            this.IsBodyHtml = true;
            this._SmtpHost = smtpHost;
            this._SmtpUseCredentials = smtpUseCredentials;
            this._SmtpUserName = smtpUserName;
            this._SmtpPassword = smtpPassword;
            this._SmtpPort = smtpPort;
            this._SmtpEnableSSL = smtpEnableSSL;
        }

        /// <summary>
        /// Enviar o email.
        /// </summary>
        /// <returns>True caso o email tenha sido enviado com sucesso.</returns>
        public void Enviar()
        {
            try
            {
                if (_SmtpClient == null)
                {
                    _SmtpClient = new SmtpClient(_SmtpHost);
                    _SmtpClient.EnableSsl = _SmtpEnableSSL;

                    // Indicar porta do SMTP.
                    if (_SmtpPort.HasValue && _SmtpPort.Value > 0)
                        _SmtpClient.Port = _SmtpPort.Value;

                    //SmtpClient.ServicePoint.Expect100Continue = false;
                    _SmtpClient.ServicePoint.MaxIdleTime = 0;

                    // Utilizar autenticação do SMTP.
                    _SmtpClient.UseDefaultCredentials = _SmtpUseCredentials;
                    if (_SmtpUseCredentials)
                        _SmtpClient.Credentials = new NetworkCredential(_SmtpUserName, _SmtpPassword);
                }

                _SmtpClient.Send(this);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public void Enviar(MailMessage mens)
        {
            try
            {
                if (_SmtpClient == null)
                {
                    _SmtpClient = new SmtpClient(_SmtpHost);
                    _SmtpClient.EnableSsl = _SmtpEnableSSL;

                    // Indicar porta do SMTP.
                    if (_SmtpPort.HasValue && _SmtpPort.Value > 0)
                        _SmtpClient.Port = _SmtpPort.Value;

                    //SmtpClient.ServicePoint.Expect100Continue = false;
                    _SmtpClient.ServicePoint.MaxIdleTime = 0;

                    // Utilizar autenticação do SMTP.
                    _SmtpClient.UseDefaultCredentials = _SmtpUseCredentials;
                    if (_SmtpUseCredentials)
                        _SmtpClient.Credentials = new NetworkCredential(_SmtpUserName, _SmtpPassword);
                }

                _SmtpClient.Send(mens);
            }
            catch
            {
                //try
                //{
                //    this.SmtpClient.Dispose();
                //    this.SmtpClient = null;
                //}
                //catch { }
                throw;
            }
            finally
            {
                //this.SmtpClient.Dispose();
                //this.SmtpClient = null;
            }
        }

        public new void Dispose()
        {
            _SmtpClient.Dispose();
            _SmtpClient = null;

            base.Dispose(true);
        }

        public void EnviarAsync(object param)
        {
            try
            {
                if (_SmtpClient == null)
                {
                    _SmtpClient = new SmtpClient(_SmtpHost);

                    // Indicar porta do SMTP.
                    if (_SmtpPort.HasValue && _SmtpPort.Value > 0)
                        _SmtpClient.Port = _SmtpPort.Value;

                    // Utilizar autenticação do SMTP.
                    _SmtpClient.UseDefaultCredentials = _SmtpUseCredentials;
                    if (_SmtpUseCredentials)
                        _SmtpClient.Credentials = new NetworkCredential(_SmtpUserName, _SmtpPassword);
                }

                // Enviar email.
                _SmtpClient.SendCompleted += new SendCompletedEventHandler(SmtpClient_SendCompleted);
                _SmtpClient.SendAsync(this, param);
            }
            catch { throw; }
            finally
            {
                _SmtpClient.Dispose();
            }
        }

        void SmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            CallEmailEnviado(e.UserState);
        }

        /// <summary>
        /// Indica se o email está em um formato válido.
        /// </summary>
        /// <param name="email">Email a ser verificado.</param>
        /// <returns>Retorna True em caso positivo.</returns>
        public static bool ValidarEmail(string email)
        {
            //Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"); 

            //if (rg.IsMatch(email))
            //    return true;

            //return false;

            if (String.IsNullOrEmpty(email))
                return false;

            if (!email.Contains("@"))
                return false;

            if (!email.Contains("."))
                return false;

            /*Regex rg = new Regex(@"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$");
            if (rg.IsMatch(email.ToLower()))
                return true;*/

            return true;
        }
    }
}
