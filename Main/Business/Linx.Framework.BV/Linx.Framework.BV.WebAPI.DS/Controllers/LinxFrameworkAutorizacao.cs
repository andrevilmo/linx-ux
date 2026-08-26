using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using Linx.Framework.BV.Autorizacao;
using Linx.Data;
using System.Web;
using System.Web.Security;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Net.Http.Headers;
using Ionic.Zip;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkAutorizacaoController
    {
        private Guid validateuser(string userName, string userPassword)
        {
            AutorizacaoDomainService ds = new AutorizacaoDomainService();
            UsuarioAutorizacao.UsuarioAutorizacaoDomainService dsUsuarioAut = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();

            try
            {
                if (!ds.ValidateUser(userName, userPassword))
                {
                    // Membership.ValidateUser returns false for locked accounts  promote to ERRAUT020.
                    if (ds.IsMembershipUserLockedOut(userName))
                        throw new Exception(ErrorConstants.FormatUserLockedOutMessage());

                    throw new Exception(ds.FormatCountableAuthFailureMessage(
                        userName,
                        ErrorConstants._UserBadNameOrPassword.Code,
                        ErrorConstants._UserBadNameOrPassword.Message));
                }
            }
            catch (Exception ex)
            {
                if (ErrorConstants.IsMembershipLockoutMessage(ex.Message) || ds.IsMembershipUserLockedOut(userName))
                    throw new Exception(ErrorConstants.FormatUserLockedOutMessage());
                throw;
            }

            Guid uidUsuario = (from result in dsUsuarioAut.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName)
                               select result.UidUsuario).FirstOrDefault();

            //Validate User (Inativo - Vigencia)
            ds.ValidateUserAccess(uidUsuario);

            return uidUsuario;
        }

        /// <summary>
        /// Portal SSO (Azure AD / MSAL): passwordless login after IdP proof.
        /// Maps UPN-prefix login (case-insensitive) to NomeAutenticacao, validates access, audits as PortalSSO.
        /// Does not accept or forward the Azure access token — identity was already proven by the Portal.
        /// </summary>
        [Route("AuthenticatePortalSso"), System.Web.Http.HttpGet()]
        public string AuthenticatePortalSso(string userName)
        {
            Linx.Security.Cryptography crypto = new Security.Cryptography();
            string userNameAttempt = userName;

            try
            {
                if (userName.IsNullOrEmpty())
                {
                    AutorizacaoDomainService dsInvalid = new AutorizacaoDomainService();
                    dsInvalid.LogAuthAccessFailure(string.Empty, ErrorConstants._LoginInvalidParameters.Code, ErrorConstants._LoginInvalidParameters.Message, "PortalSSO", false);
                    return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt("0"), crypto.Encrypt(String.Format("{0} - {1}", ErrorConstants._LoginInvalidParameters.Code, ErrorConstants._LoginInvalidParameters.Message)))));
                }

                AutorizacaoDomainService dsAuth = new AutorizacaoDomainService();
                if (dsAuth.IsMembershipUserLockedOut(userName))
                    throw new Exception(ErrorConstants.FormatUserLockedOutMessage());

                UsuarioAutorizacao.UsuarioAutorizacaoDomainService dsUsuarioAut = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
                var usuario = (
                    from result in dsUsuarioAut.GetTcsUsuarioAutenticacaoNoAssociations()
                    where result.NomeAutenticacao.ToUpper() == userName.ToUpper()
                    select new
                    {
                        UidUsuario = result.UidUsuario,
                        Usuario = result.NomeUsuario,
                        NomeCurto = result.NomeCurtoUsuario,
                        NomeAutenticacao = result.NomeAutenticacao
                    }).FirstOrDefault();

                if (usuario.IsNull())
                {
                    dsAuth.LogAuthAccessFailure(userName, ErrorConstants._UserBadNameOrPassword.Code,
                        "Usuário autenticado no Azure, mas sem cadastro local. Ajuste o login na retaguarda.", "PortalSSO", false);
                    return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt("0"),
                        crypto.Encrypt("Usuário autenticado no Azure, mas sem cadastro local. Ajuste o login na retaguarda."))));
                }

                // Validate User (Inativo - Vigencia) — same gates as password login, without Membership password.
                dsAuth.ValidateUserAccess(usuario.UidUsuario);

                dsAuth.LogAuthAccessSuccess(usuario.NomeAutenticacao, "PortalSSO");

                return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}||{2}||{3}",
                    crypto.Encrypt("1"),
                    crypto.Encrypt(usuario.Usuario),
                    crypto.Encrypt(usuario.NomeCurto),
                    crypto.Encrypt(usuario.NomeAutenticacao))));
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception oException)
            {
                string errorMessage = ErrorConstants.EnsureUserLockedOutMessage(oException.Message);
                try
                {
                    AutorizacaoDomainService ds = new AutorizacaoDomainService();
                    if (!userNameAttempt.IsNullOrEmpty() && ds.IsMembershipUserLockedOut(userNameAttempt))
                        errorMessage = ErrorConstants.FormatUserLockedOutMessage();
                    else
                        ds.LogAuthAccessFailure(userNameAttempt ?? string.Empty, null, errorMessage, "PortalSSO", false);
                }
                catch { }

                return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt("0"), crypto.Encrypt(errorMessage))));
            }
        }

        [Route("AuthenticatePortal"), System.Web.Http.HttpGet()]
        public string AuthenticatePortal(string authenticateParameters)
        {
            Linx.Security.Cryptography crypto = new Security.Cryptography();
            string userNameAttempt = null;

            try
            {
                string[] decryptedLines = crypto.Decrypt(authenticateParameters).Split(new string[] { "||" }, StringSplitOptions.None);

                if (decryptedLines.Count() != 2)
                {
                    AutorizacaoDomainService dsInvalid = new AutorizacaoDomainService();
                    dsInvalid.LogAuthAccessFailure(string.Empty, ErrorConstants._LoginInvalidParameters.Code, ErrorConstants._LoginInvalidParameters.Message, "Portal", false);
                    return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt("0"), crypto.Encrypt(String.Format("{0} - {1}", ErrorConstants._LoginInvalidParameters.Code, ErrorConstants._LoginInvalidParameters.Message)))));
                }

                userNameAttempt = crypto.Decrypt(decryptedLines[0]);
                Guid uidUsuario = this.validateuser(userNameAttempt, crypto.Decrypt(decryptedLines[1]));

                UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();

                var usuario = (
                    from result in ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.UidUsuario == uidUsuario)
                    select new { Usuario = result.NomeUsuario, NomeCurto = result.NomeCurtoUsuario, NomeAutenticacao = result.NomeAutenticacao }).FirstOrDefault();

                AutorizacaoDomainService dsAuth = new AutorizacaoDomainService();
                dsAuth.LogAuthAccessSuccess(usuario != null ? usuario.NomeAutenticacao : userNameAttempt, "Portal");

                return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}||{2}", crypto.Encrypt("1"), crypto.Encrypt(usuario.Usuario), crypto.Encrypt(usuario.NomeCurto))));
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception oException)
            {
                string errorMessage = ErrorConstants.EnsureUserLockedOutMessage(oException.Message);
                try
                {
                    // If Membership locked the account, always return ERRAUT020 (Portuguese).
                    string[] decryptedLines = crypto.Decrypt(authenticateParameters).Split(new string[] { "||" }, StringSplitOptions.None);
                    if (decryptedLines.Count() == 2)
                    {
                        string userName = crypto.Decrypt(decryptedLines[0]);
                        AutorizacaoDomainService ds = new AutorizacaoDomainService();
                        if (ds.IsMembershipUserLockedOut(userName))
                            errorMessage = ErrorConstants.FormatUserLockedOutMessage();
                    }
                }
                catch { }

                return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt("0"), crypto.Encrypt(errorMessage))));
            }
        }

        [Route("AuthenticateUser"), System.Web.Http.HttpGet()]
        public LoginInfo AuthenticateUser(string authenticatedUser, Guid applicationId, Guid companyId, Guid accessGroupId, int environmentId)
        {
            AutorizacaoDomainService context = new AutorizacaoDomainService();
            return context.AuthenticateUser(authenticatedUser, applicationId, companyId, accessGroupId, environmentId);
        }

        [Route("RecoverUserPassword"), System.Web.Http.HttpGet()]
        public bool RecoverUserPassword(string userName)
        {
            AutorizacaoDomainService context = new AutorizacaoDomainService();
            return context.RecoverUserPassword(userName);
        }

        [Route("SendPasswordResetLink"), System.Web.Http.HttpGet()]
        public bool SendPasswordResetLink(string userName, string callbackUrl)
        {
            AutorizacaoDomainService context = new AutorizacaoDomainService();
            return context.SendPasswordResetLink(userName, callbackUrl);
        }

        [Route("ValidatePasswordResetToken"), System.Web.Http.HttpGet()]
        public bool ValidatePasswordResetToken(string token)
        {
            AutorizacaoDomainService context = new AutorizacaoDomainService();
            return context.ValidatePasswordResetToken(token);
        }

        [Route("ResetPasswordWithToken"), System.Web.Http.HttpGet()]
        public bool ResetPasswordWithToken(string token, string newPassword)
        {
            AutorizacaoDomainService context = new AutorizacaoDomainService();
            return context.ResetPasswordWithToken(token, newPassword);
        }

        [Route("IsMembershipUserLockedOut"), System.Web.Http.HttpGet()]
        public bool IsMembershipUserLockedOut(string userName)
        {
            AutorizacaoDomainService context = new AutorizacaoDomainService();
            return context.IsMembershipUserLockedOut(userName);
        }

        [Route("UnlockMembershipUser"), System.Web.Http.HttpGet()]
        public bool UnlockMembershipUser(string userName)
        {
            AutorizacaoDomainService context = new AutorizacaoDomainService();
            return context.UnlockMembershipUser(userName);
        }

        [Route("GetMfaStatus"), System.Web.Http.HttpGet()]
        public MfaStatusResult GetMfaStatus(string tableOrigin, int idGpecon, long idUserMfa = 0, Guid? uidUsuario = null)
        {
            return new AutorizacaoDomainService().GetMfaStatus(tableOrigin, idGpecon, idUserMfa, uidUsuario);
        }

        [Route("BeginMfaEnrollment"), System.Web.Http.HttpGet()]
        public object BeginMfaEnrollment(string tableOrigin, int idGpecon, long idUserMfa = 0, Guid? uidUsuario = null)
        {
            MfaEnrollResult enroll = new AutorizacaoDomainService().BeginMfaEnrollment(tableOrigin, idGpecon, idUserMfa, uidUsuario);
            string qr = null;
            if (enroll.Success && !string.IsNullOrEmpty(enroll.OtpauthUri))
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                using (Bitmap bitmap = GetQrCode(enroll.OtpauthUri))
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    qr = Convert.ToBase64String(stream.ToArray());
                }
            }
            return new
            {
                enroll.Success,
                enroll.Message,
                enroll.OtpauthUri,
                enroll.AccountLabel,
                QrCodePngBase64 = qr
            };
        }

        [Route("ConfirmMfaEnrollment"), System.Web.Http.HttpGet()]
        public MfaValidateResult ConfirmMfaEnrollment(string tableOrigin, int idGpecon, string code, long idUserMfa = 0, Guid? uidUsuario = null)
        {
            return new AutorizacaoDomainService().ConfirmMfaEnrollment(tableOrigin, idGpecon, idUserMfa, uidUsuario, code);
        }

        [Route("ValidateMfaCode"), System.Web.Http.HttpGet()]
        public MfaValidateResult ValidateMfaCode(string tableOrigin, int idGpecon, string code, long idUserMfa = 0, Guid? uidUsuario = null, string canal = null)
        {
            return new AutorizacaoDomainService().ValidateMfaCode(tableOrigin, idGpecon, idUserMfa, uidUsuario, code, canal);
        }

        [Route("RevokeMfaSecret"), System.Web.Http.HttpGet()]
        public MfaValidateResult RevokeMfaSecret(string tableOrigin, int idGpecon, long idUserMfa = 0, Guid? uidUsuario = null)
        {
            return new AutorizacaoDomainService().RevokeMfaSecret(tableOrigin, idGpecon, idUserMfa, uidUsuario);
        }

        [Route("GetMfaCompanyPolicy"), System.Web.Http.HttpGet()]
        public MfaCompanyPolicy GetMfaCompanyPolicy(int idGpecon)
        {
            return new AutorizacaoDomainService().GetMfaCompanyPolicy(idGpecon);
        }

        [Route("SetMfaCompanyPolicy"), System.Web.Http.HttpGet()]
        public MfaCompanyPolicy SetMfaCompanyPolicy(int idGpecon, bool indicaMfaHabilitado, bool indicaDispositivoConfiavel = false, int qtdDiasConfianca = 0)
        {
            return new AutorizacaoDomainService().SetMfaCompanyPolicy(idGpecon, indicaMfaHabilitado, indicaDispositivoConfiavel, qtdDiasConfianca);
        }

        [Route("SetUserMfaFlags"), System.Web.Http.HttpGet()]
        public MfaStatusResult SetUserMfaFlags(Guid uidUsuario, bool? utilizaSso = null, bool? utilizaMfa = null)
        {
            return new AutorizacaoDomainService().SetUserMfaFlags(uidUsuario, utilizaSso, utilizaMfa);
        }

        [Route("LinkMfaDevice"), System.Web.Http.HttpGet()]
        public MfaDeviceResult LinkMfaDevice(string tableOrigin, int idGpecon, long idUserMfa = 0, Guid? uidUsuario = null, string userAgent = null)
        {
            return new AutorizacaoDomainService().LinkMfaDevice(tableOrigin, idGpecon, idUserMfa, uidUsuario, userAgent);
        }

        [Route("CheckMfaDevice"), System.Web.Http.HttpGet()]
        public bool CheckMfaDevice(string tableOrigin, int idGpecon, string deviceToken, long idUserMfa = 0, Guid? uidUsuario = null)
        {
            return new AutorizacaoDomainService().CheckMfaDevice(tableOrigin, idGpecon, idUserMfa, uidUsuario, deviceToken);
        }

        [Route("ValidateMfaTicket"), System.Web.Http.HttpGet()]
        public MfaValidateResult ValidateMfaTicket(string ticket)
        {
            return new AutorizacaoDomainService().ValidateMfaTicket(ticket);
        }

        [Route("IssueMfaSkipTicket"), System.Web.Http.HttpGet()]
        public MfaValidateResult IssueMfaSkipTicket(string tableOrigin, int idGpecon, long idUserMfa = 0, Guid? uidUsuario = null, string reason = null)
        {
            return new AutorizacaoDomainService().IssueMfaSkipTicket(tableOrigin, idGpecon, idUserMfa, uidUsuario, reason);
        }


        [Route("AuthenticateWindowsApp"), System.Web.Http.HttpGet()]
        public List<UsuarioAcesso> AuthenticateWindowsApp(string userName, string userPassword)
        {
            List<UsuarioAcesso> acessos = new List<UsuarioAcesso>();

            Guid uidUsuario = this.validateuser(userName, userPassword);

            UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
            //aqui
            //acessos = (from result in ds.GetTcsUsuarioAcessoNoAssociations().Where(i => i.IdAplicativo == 1 && i.UidUsuario == uidUsuario)
            acessos = (from result in ds.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.UidUsuario == uidUsuario)
                       select new UsuarioAcesso()
                       {
                           UidUsuario = result.UidUsuario,
                           NomeUsuario = result.NomeUsuario,
                           IdAmbiente = result.IdTcsAmbiente,
                           DescricaoAmbiente = result.DescricaoAmbiente,
                           UidAplicacao = result.UidAplicacao,
                           DescricaoAplicacao = result.DescricaoAplicacao,
                           UidEmpresa = result.UidEmpresa,
                           NomeEmpresa = result.NomeEmpresa,
                           UidGrupoEconomico = result.UidGrupoEconomico,
                           DescricaoGrupoEconomico = result.GrupoEconomico,
                           //UidGrupoAcesso = result.UidGrupoAcesso,
                           //DescricaoGrupoAcesso = result.DescricaoGrupo,
                           UrlAplicacao = result.Url,
                           IdLinxGpecon = result.IdLinxGpecon
                       }).ToList();

            new AutorizacaoDomainService().LogAuthAccessSuccess(userName, "WindowsApp");
            return acessos;
        }

        [Route("ChangeUserPassword"), System.Web.Http.HttpGet()]
        public bool ChangeUserPassword(Guid userUid, string oldPassword, string newPassword)
        {
            AutorizacaoDomainService ds = new AutorizacaoDomainService();
            return ds.ChangeUserPassword(userUid, oldPassword, newPassword);
        }

        [Route("AuthenticateLocalBus"), System.Web.Http.HttpGet()]
        public AuthenticationResult AuthenticateLocalBus(string deviceId, string encodedSecret)
        {
            if (Linx.Tools.LocalServiceBus.Enabled)
                return Linx.Tools.LocalServiceBus.GetAuthenticationByDevice(deviceId, encodedSecret);
            else
                return new AuthenticationResult();
        }

        [Route("GetLocalBusSecurityKeys"), System.Web.Http.HttpGet()]
        public IList<string> GetLocalBusSecurityKeys(string deviceId)
        {
            if (Linx.Tools.LocalServiceBus.Enabled)
                return Linx.Tools.LocalServiceBus.GetSecurityKeys(deviceId);
            else
                return new string[] { };
        }

        [Route("GetLocalBusFingerPrint"), System.Web.Http.HttpGet()]
        public string GetLocalBusFingerPrint()
        {
            //if (Linx.Tools.LocalServiceBus.Enabled)
            //{
            var b32 = new Base32Url(true);
            return b32.Encode(System.Text.Encoding.ASCII.GetBytes(Linx.Tools.LocalServiceBus.FingerPrint + "||" + DateTime.UtcNow.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fffZ")));
            //}
            //else
            //                return "";
        }

        [Route("GetLocalBusSetup"), System.Web.Http.HttpGet()]
        public HttpResponseMessage GetLocalBusSetup(string appName = null, bool isAPK = false)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (Linx.Tools.LocalServiceBus.Enabled)
            {
                string[] urlResult = Request.RequestUri.AbsoluteUri.ToString().Split('/');
                string serviceBus = urlResult[0] + "//" + urlResult[2] + "/";

                String busJson = (isAPK ? serviceBus + "apps/host.apk" : "{\"URL\":\"" + serviceBus + "\"" + (appName.IsNullOrEmpty() ? "" : ",\"APPNAME\":\"" + appName + "\"") + "}");

                MemoryStream strJason = new MemoryStream();
                Bitmap imgJason = GetQrCode(busJson);
                imgJason.Save(strJason, System.Drawing.Imaging.ImageFormat.Png);
                httpResponseMessage.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                httpResponseMessage.Content = new StreamContent(new MemoryStream(strJason.ToArray()));
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/PNG");
            }

            return httpResponseMessage;
        }

        private static Bitmap GetQrCode(String content)
        {
            QRCodeEncoder qrCodecEncoder = new QRCodeEncoder();
            qrCodecEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
            qrCodecEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;
            qrCodecEncoder.CharacterSet = "UTF-8";
            qrCodecEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodecEncoder.QRCodeScale = 6;
            qrCodecEncoder.QRCodeVersion = 0;
            qrCodecEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            Bitmap imageQRCode = qrCodecEncoder.Encode(content);
            return imageQRCode;
        }

        [Route("GetAppInfo"), System.Web.Http.HttpGet()]
        public Autorizacao.AppInfo GetAppInfo(string appName)
        {
            try
            {
                AppInfo appInfo = new AppInfo();
                string appsPath = string.Empty;

                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                    appsPath = System.Web.Hosting.HostingEnvironment.MapPath("~/apps");
                else
                    appsPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\apps";

                if (!Directory.Exists(appsPath))
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                var searchPattern = string.Concat("app_", appName, "*.zip");
                var appZipName = Directory.EnumerateFiles(appsPath, searchPattern, SearchOption.TopDirectoryOnly).OrderByDescending(s => s, StringComparer.CurrentCultureIgnoreCase).FirstOrDefault();
                string r = null;

                if (appZipName == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                using (ZipFile zip = ZipFile.Read(appZipName))
                {
                    r = zip.Comment;
                }

                appInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<AppInfo>(r);

                return appInfo;
            }
            catch (HttpResponseException httpException)
            {
                throw new HttpResponseException(httpException.Response.StatusCode);
            }
            catch (Exception oException)
            {
                throw new Exception(oException.Message);
            }
        }

        [HttpGet()]
        [Route("UpdateAuthentication")]
        public bool UpdateAuthentication(string parameters)
        {
            if (LocalServiceBus.Enabled)
            {
                LocalServiceBus.Start();
                return LocalServiceBus.Enabled;
            }

            return true;
        }
    }
}
