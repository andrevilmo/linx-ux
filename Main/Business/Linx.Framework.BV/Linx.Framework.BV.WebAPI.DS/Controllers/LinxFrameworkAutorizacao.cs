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

            if (!ds.ValidateUser(userName, userPassword))
                throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));

            Guid uidUsuario = (from result in dsUsuarioAut.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName)
                               select result.UidUsuario).FirstOrDefault();

            //Validate User (Inativo - Vigência)
            ds.ValidateUserAccess(uidUsuario);

            return uidUsuario;
        }

        [Route("AuthenticatePortal"), System.Web.Http.HttpGet()]
        public string AuthenticatePortal(string authenticateParameters)
        {
            Linx.Security.Cryptography crypto = new Security.Cryptography();

            try
            {
                string[] decryptedLines = crypto.Decrypt(authenticateParameters).Split(new string[] { "||" }, StringSplitOptions.None);

                if (decryptedLines.Count() != 2)
                {
                    return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt("0"), crypto.Encrypt(String.Format("{0} - {1}", ErrorConstants._LoginInvalidParameters.Code, ErrorConstants._LoginInvalidParameters.Message)))));
                }

                Guid uidUsuario = this.validateuser(crypto.Decrypt(decryptedLines[0]), crypto.Decrypt(decryptedLines[1]));

                UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();

                var usuario = (
                    from result in ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.UidUsuario == uidUsuario)
                    select new { Usuario = result.NomeUsuario, NomeCurto = result.NomeCurtoUsuario }).FirstOrDefault();

                return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}||{2}", crypto.Encrypt("1"), crypto.Encrypt(usuario.Usuario), crypto.Encrypt(usuario.NomeCurto))));
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception oException)
            {
                return HttpUtility.UrlEncode(crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt("0"), crypto.Encrypt(oException.Message))));
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
