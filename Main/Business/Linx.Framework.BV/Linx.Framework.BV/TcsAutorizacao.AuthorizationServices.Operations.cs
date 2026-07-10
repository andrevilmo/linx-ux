using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.Autorizacao.BM;
using System.Web;
using System.Web.Security;
using Linx.Resources.Localization.Strings;
using Linx.Resources.Localization.Security;
using System.Transactions;
using Linx.TCS0101.BO.TcsParametro;

namespace Linx.TCS0101.BO.TcsAutorizacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsAutorizacaoDomainService
    {
        [Invoke(HasSideEffects = true)]
        public Guid AuthenticateUser(string authenticatedUser, Guid applicationId, Guid companyId, Guid accessGroupId)
        {
            try
            {
                var query = from result in this.DbContext.TCS_USUARIO_ACESSO
                            where result.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO.ToUpper() == authenticatedUser.ToUpper() && result.UID_APLICACAO == applicationId && result.UID_EMPRESA == companyId && result.UID_GRUPO_ACESSO == accessGroupId
                            select new { UidUsuario = result.UID_USUARIO, UidAplicacao = result.UID_APLICACAO, UidEmpresa = result.UID_EMPRESA, UidGrupoAcesso = result.UID_GRUPO_ACESSO};

                if (query.Count() > 0)
                {
                    var usuarioAcesso = query.First();

                    //UserInfo
                    TCS_USUARIO_AUTENTICACAO user = this.GetUser(usuarioAcesso.UidUsuario);

                    return this.UpdateToken(usuarioAcesso.UidUsuario, usuarioAcesso.UidAplicacao, usuarioAcesso.UidEmpresa, usuarioAcesso.UidGrupoAcesso);
                }
                else
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));
            }
            catch (Exception oException)
            {
                string errorMessage = oException.InnerException.IsNull() ? oException.Message : oException.InnerException.Message;
                throw new DomainException(errorMessage);
            }
        }

        [Invoke(HasSideEffects = true)]
        public bool ValidateToken(Guid userId, Guid authorizationToken, Guid applicationId, Guid companyId, Guid accessGroupId)
        {
            Object appCache = WebCacheHelper.GetWebCache("AllowedApplications");

            List<Guid> allowedApps;

            if (appCache.IsNull())
            {
                var query = from result in this.DbContext.TCS_APLICACAO
                            where result.ID_APLICATIVO == 15
                            select result.UID_APLICACAO;

                allowedApps = query.ToList();
                WebCacheHelper.AddWebCache("AllowedApplications", allowedApps);
            }
            else
            {
                allowedApps = appCache as List<Guid>;
            }

            if (allowedApps.Where(i => i == applicationId).Count() > 0)
                return true;

            Object cache = WebCacheHelper.GetWebCache(userId.ToString());

            if (cache.IsNull())
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._AuthorizationTokenNotFound.Code, ErrorConstants._AuthorizationTokenNotFound.Message));

            List<Acesso> TokenList = cache as List<Acesso>;
            Acesso acesso = TokenList.Where(i => i.UidAplicacao == applicationId && i.UidEmpresa == companyId && i.Token == authorizationToken && i.UidGrupoAcesso == accessGroupId).FirstOrDefault();

            if (acesso.IsNull())
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._AuthorizationTokenExpired.Code, ErrorConstants._AuthorizationTokenExpired.Message));

            return true;
        }

        [Invoke(HasSideEffects = true)]
        private Guid UpdateToken(Guid userId, Guid applicationId, Guid companyId, Guid accessGroupId)
        {
            Object cache = WebCacheHelper.GetWebCache(userId.ToString());
            List<Acesso> TokenList;
            Guid token = Guid.NewGuid();
            if (cache.IsNull())
            {
                TokenList = new List<Acesso>();
                TokenList.Add(new Acesso() { UidEmpresa = companyId, Token = token, UidAplicacao = applicationId, UidGrupoAcesso = accessGroupId });
                WebCacheHelper.AddWebCache(userId.ToString(), TokenList);
            }
            else
            {
                TokenList = cache as List<Acesso>;

                Acesso acesso = TokenList.Where(i => i.UidEmpresa == companyId && i.UidAplicacao == applicationId && i.UidGrupoAcesso == accessGroupId).FirstOrDefault();

                if (acesso.IsNull())
                    TokenList.Add(new Acesso() { UidEmpresa = companyId, Token = token, UidAplicacao = applicationId, UidGrupoAcesso = accessGroupId });
                else
                    acesso.Token = token;

                WebCacheHelper.UpdateWebCache(userId.ToString(), TokenList);

            }
            return token;
        }

        [Invoke(HasSideEffects = false)]
        public Dictionary<int, string> AuthenticateJson(string userName, string password, string applicationId)
        {
            return this.AuthenticatePos(userName, password, applicationId);
        }

        [Invoke(HasSideEffects = true)]
        protected internal TCS_USUARIO_AUTENTICACAO GetUser(Guid uidUsuario)
        {
            DateTime currentDate = DateTime.Now.Date;

            TCS_USUARIO_AUTENTICACAO usuario = 
                                            (from result in this.DbContext.TCS_USUARIO_AUTENTICACAO
                                                where result.UID_USUARIO == uidUsuario
                                                select result).FirstOrDefault();
            if (usuario.IsNullOrEmpty())
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));

            if (usuario.INATIVO)
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotActive.Code, ErrorConstants._UserNotActive.Message));

            if (usuario.VIGENCIA_INICIAL.Date > currentDate || usuario.VIGENCIA_FINAL.Date < currentDate)
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserLoginExpired.Code, ErrorConstants._UserLoginExpired.Message));

            return usuario;
        }

        [Invoke(HasSideEffects = true)]
        protected internal static string EmailBody(string userName, string authenticationName, string password)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\LinxMail\";
                var webClient = new System.Net.WebClient();
                string successHtml = webClient.DownloadString(path + "SendPasswordMailBody.html");
                successHtml = successHtml.Replace("{0}", userName);
                successHtml = successHtml.Replace("{1}", authenticationName);
                successHtml = successHtml.Replace("{2}", password);
                return successHtml;
            }
            catch (Exception oException)
            {
                throw new DomainException("Erro ao gerar corpo do email para envio de senha.\n\n".Translate(), oException);
            }
        }

        [Invoke(HasSideEffects = true)]
        public string GetAspNetUser(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            return user.IsNullOrEmpty() ? null : user.UserName;
        }

        [Invoke(HasSideEffects = true)]
        public bool ChangeUserPassword(Guid userUid, string oldPassword, string newPassword)
        {
            bool passwordChanged = false;
            string error = string.Empty;
            try
            {
                TcsAutorizacaoDomainService ds = new TcsAutorizacaoDomainService();
                TcsAutorizacao.TcsUsuarioAutenticacao usuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.UidUsuario == userUid).FirstOrDefault();

                if (usuario.IsNullOrEmpty())
                {
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
                }

                //Validates user and current password.
                if (!Membership.ValidateUser(usuario.NomeAutenticacao, oldPassword))
                {
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));
                }

                //New password must be different from the older one.
                if (oldPassword == newPassword)
                {
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._PasswordSameAsCurrent.Code, ErrorConstants._PasswordSameAsCurrent.Message));
                }

                using (TransactionScope transaction = new TransactionScope())
                {
                    MembershipUser user = Membership.GetUser(usuario.NomeAutenticacao);
                    passwordChanged = user.ChangePassword(user.ResetPassword("Dog"), newPassword);
                    if (!passwordChanged)
                    {
                        throw new Exception(String.Format("{0} - {1}", ErrorConstants._ChangePasswordError.Code, ErrorConstants._ChangePasswordError.Message));
                    }

                    int expirationDays = Linx.TCS0101.BO.LinxBusinessParameters.GetParameter<int>("DIAS_EXPIRACAO_SENHA_USUARIO", null);
                    
                    if (expirationDays == 0)
                        expirationDays = 90;

                    usuario.DataExpiracaoSenha = DateTime.Now.Date.AddDays(expirationDays);
                    ds.AddCustomChanges(usuario, null, ChangeOperation.Update);
                    ds.SaveCustomChanges();
                    transaction.Complete();
                }
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
            return passwordChanged;
        }

        [Invoke(HasSideEffects = true)]
        public bool RecoverUserPassword(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);

            if (user.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            using (TransactionScope transaction = new TransactionScope())
            {
                string password = user.ResetPassword("Dog");

                TcsAutorizacaoDomainService ds = new TcsAutorizacaoDomainService();
                TcsUsuarioAutenticacao usuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName).FirstOrDefault();

                if (usuario.IsNullOrEmpty())
                {
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
                }

                usuario.DataExpiracaoSenha = DateTime.Now.Date;
                ds.AddCustomChanges(usuario, null, ChangeOperation.Update);
                ds.SaveCustomChanges();

                Linx.Tools.LinxMail.Send(usuario.Email, "Recupera??o de senha de usu?rio.".Translate(), true, TcsAutorizacaoDomainService.EmailBody(usuario.NomeUsuario, usuario.NomeAutenticacao, password));

                transaction.Complete();
            }

            return true;
        }

        [Invoke(HasSideEffects = false)]
        public bool RecoverUserPasswordJson(string userName)
        {
            return this.RecoverUserPassword(userName);
        }

        private const int _PasswordResetTokenValidityMinutes = 60;

        [Invoke(HasSideEffects = true)]
        public bool SendPasswordResetLink(string userName, string callbackUrl)
        {
            MembershipUser user = Membership.GetUser(userName);

            if (user.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            TcsAutorizacaoDomainService ds = new TcsAutorizacaoDomainService();
            TcsUsuarioAutenticacao usuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName).FirstOrDefault();

            if (usuario.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            string token = GeneratePasswordResetToken(userName, user);

            string separator = (!callbackUrl.IsNullOrEmpty() && callbackUrl.IndexOf('?') >= 0) ? "&" : "?";
            string link = string.Format("{0}{1}token={2}", callbackUrl, separator, System.Web.HttpUtility.UrlEncode(token));

            Linx.Tools.LinxMail.Send(usuario.Email, "Redefini??o de senha de usu?rio.".Translate(), true, ResetPasswordEmailBody(usuario.NomeUsuario, link));

            return true;
        }

        [Invoke(HasSideEffects = false)]
        public bool ValidatePasswordResetToken(string token)
        {
            string userName;
            return TryValidatePasswordResetToken(token, out userName);
        }

        [Invoke(HasSideEffects = true)]
        public bool ResetPasswordWithToken(string token, string newPassword)
        {
            string userName;
            if (!TryValidatePasswordResetToken(token, out userName))
            {
                throw new DomainException("Link de redefini??o de senha inv?lido ou expirado.".Translate());
            }

            TcsAutorizacaoDomainService ds = new TcsAutorizacaoDomainService();
            TcsUsuarioAutenticacao usuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName).FirstOrDefault();

            if (usuario.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            MembershipUser user = Membership.GetUser(userName);

            if (user.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            using (TransactionScope transaction = new TransactionScope())
            {
                bool passwordChanged = user.ChangePassword(user.ResetPassword("Dog"), newPassword);
                if (!passwordChanged)
                {
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._ChangePasswordError.Code, ErrorConstants._ChangePasswordError.Message));
                }

                int expirationDays = Linx.TCS0101.BO.LinxBusinessParameters.GetParameter<int>("DIAS_EXPIRACAO_SENHA_USUARIO", null);

                if (expirationDays == 0)
                    expirationDays = 90;

                usuario.DataExpiracaoSenha = DateTime.Now.Date.AddDays(expirationDays);
                ds.AddCustomChanges(usuario, null, ChangeOperation.Update);
                ds.SaveCustomChanges();

                transaction.Complete();
            }

            return true;
        }

        [Invoke(HasSideEffects = false)]
        public bool SendPasswordResetLinkJson(string userName, string callbackUrl)
        {
            return this.SendPasswordResetLink(userName, callbackUrl);
        }

        [Invoke(HasSideEffects = false)]
        public bool ValidatePasswordResetTokenJson(string token)
        {
            return this.ValidatePasswordResetToken(token);
        }

        [Invoke(HasSideEffects = false)]
        public bool ResetPasswordWithTokenJson(string token, string newPassword)
        {
            return this.ResetPasswordWithToken(token, newPassword);
        }

        private static string GeneratePasswordResetToken(string userName, MembershipUser user)
        {
            long expirationTicks = DateTime.UtcNow.AddMinutes(_PasswordResetTokenValidityMinutes).Ticks;
            long stampTicks = user.LastPasswordChangedDate.Ticks;
            string payload = string.Format("{0}||{1}||{2}", userName, expirationTicks, stampTicks);

            Linx.Security.Cryptography crypto = new Linx.Security.Cryptography();
            return crypto.Encrypt(payload);
        }

        private static bool TryValidatePasswordResetToken(string token, out string userName)
        {
            userName = null;

            if (token.IsNullOrEmpty())
                return false;

            string payload;
            try
            {
                Linx.Security.Cryptography crypto = new Linx.Security.Cryptography();
                payload = crypto.Decrypt(token);
            }
            catch
            {
                return false;
            }

            if (payload.IsNullOrEmpty())
                return false;

            string[] parts = payload.Split(new string[] { "||" }, StringSplitOptions.None);
            if (parts.Length != 3)
                return false;

            long expirationTicks;
            long stampTicks;
            if (!long.TryParse(parts[1], out expirationTicks) || !long.TryParse(parts[2], out stampTicks))
                return false;

            if (DateTime.UtcNow.Ticks > expirationTicks)
                return false;

            MembershipUser user = Membership.GetUser(parts[0]);
            if (user.IsNullOrEmpty())
                return false;

            if (user.LastPasswordChangedDate.Ticks != stampTicks)
                return false;

            userName = parts[0];
            return true;
        }

        private static string ResetPasswordEmailBody(string userName, string link)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head><body style=\"font-family:'Segoe UI';font-size:12px;color:#333;\">");
            sb.AppendFormat("<p>Olá {0},</p>", System.Web.HttpUtility.HtmlEncode(userName));
            sb.Append("<p>Recebemos uma solicitação para redefinir a sua senha de acesso.</p>");
            sb.AppendFormat("<p><a href=\"{0}\" style=\"color:#0000FF;font-weight:bold;\">Clique aqui para redefinir a sua senha</a>.</p>", link);
            sb.Append("<p>Se o botão não funcionar, copie e cole o endereço abaixo no seu navegador:</p>");
            sb.AppendFormat("<p>{0}</p>", System.Web.HttpUtility.HtmlEncode(link));
            sb.AppendFormat("<p>Este link expira em {0} minutos.</p>", _PasswordResetTokenValidityMinutes);
            sb.Append("<p>Se você não solicitou a redefinição, ignore este e-mail.</p>");
            sb.Append("</body></html>");
            return sb.ToString();
        }

        [Query(HasSideEffects = true)]
        protected internal IQueryable<TcsTransacaoAutorizacao> GetTcsTransacao(string transaction, string boName)
        {
            List<string> boList = boName.IsNull() ? new List<string>() : boName.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            return this.GetTcsTransacaoAutorizacaoNoAssociations().Where(i => !i.Inativo && (boList.Contains(i.ObjetoClasseNome) || i.ClasseNome == transaction));
        }

        [Invoke(HasSideEffects = true)]
        public Dictionary<int, string> AuthenticatePos(string userName, string password, string applicationId)
        {
            try
            {
                //1 -> CompanyId
                //2 -> Token
                //3 -> UserId
                //4 -> AccessGroup
                Dictionary<int, string> currentInfo = new Dictionary<int, string>();

                Guid applicationUid = Guid.Parse(applicationId);

                if (!Membership.ValidateUser(userName, password))
                {
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));
                }

                var query = from result in this.DbContext.TCS_USUARIO_ACESSO
                            where result.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO.ToUpper() == userName.ToUpper() && result.UID_APLICACAO == applicationUid
                            select new { UidEmpresa = result.UID_EMPRESA, UidUsuario = result.UID_USUARIO, UidGrupoAcesso = result.UID_GRUPO_ACESSO, UidAplicacao = result.UID_APLICACAO };

                if (query.Count() > 0)
                {
                    var usuarioAcesso = query.First();

                    //UserInfo
                    TCS_USUARIO_AUTENTICACAO user = this.GetUser(usuarioAcesso.UidUsuario);

                    Guid token = this.UpdateToken(usuarioAcesso.UidUsuario, usuarioAcesso.UidAplicacao, usuarioAcesso.UidEmpresa, usuarioAcesso.UidGrupoAcesso);
                    currentInfo.Add(1, usuarioAcesso.UidEmpresa.ToString());
                    currentInfo.Add(2, token.ToString());
                    currentInfo.Add(3, usuarioAcesso.UidUsuario.ToString());
                    currentInfo.Add(4, usuarioAcesso.UidGrupoAcesso.ToString());
                }

                if (currentInfo.Count == 0)
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));

                return currentInfo;
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message);
            }

        }

        [Invoke(HasSideEffects = true)]
        public Dictionary<int, string> AuthenticateStandAloneService(string userName, string password, string applicationId, string userId, string userApplicationId)
        {
            try
            {
                //1 -> CompanyId
                //2 -> Token
                //3 -> UserId
                //4 -> AccessGroup
                Dictionary<int, string> currentInfo = new Dictionary<int, string>();

                int[] allowedApplications = { 14, 15 };

                if (!Membership.ValidateUser(userName, password))
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));

                Guid uidServerApplication = Guid.Parse(applicationId);
                Guid uidUserApplication = Guid.Parse(userApplicationId);
                Guid uidUsuario = Guid.Parse(userId);

                //Validate application access
                var appAccess = (from result in this.DbContext.TCS_USUARIO_ACESSO
                                 where result.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO.ToUpper() == userName.ToUpper() && result.UID_APLICACAO == uidServerApplication
                                 select new { UidUsuario = result.UID_USUARIO, IdAplicativo = result.TCS_APLICACAO.ID_APLICATIVO }).FirstOrDefault();

                if (appAccess.IsNull() || !allowedApplications.Contains(appAccess.IdAplicativo))
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));

                var serverUser = this.GetUser(appAccess.UidUsuario);

                //User Access
                var userAccess = (from result in this.DbContext.TCS_USUARIO_ACESSO
                                  where result.UID_USUARIO == uidUsuario && result.UID_APLICACAO == uidUserApplication
                                  select new { UidEmpresa = result.UID_EMPRESA, UidUsuario = result.UID_USUARIO, UidGrupoAcesso = result.UID_GRUPO_ACESSO, UidAplicacao = result.UID_APLICACAO }).FirstOrDefault();
            
                if (userAccess.IsNull())
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));

                var user = this.GetUser(userAccess.UidUsuario);

                Guid token = this.UpdateToken(userAccess.UidUsuario, userAccess.UidAplicacao, userAccess.UidEmpresa, userAccess.UidGrupoAcesso);
                currentInfo.Add(1, userAccess.UidEmpresa.ToString());
                currentInfo.Add(2, token.ToString());
                currentInfo.Add(3, userAccess.UidUsuario.ToString());
                currentInfo.Add(4, userAccess.UidGrupoAcesso.ToString());

                return currentInfo;

            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message);
            }
        }

        [Invoke(HasSideEffects = false)]
        public Dictionary<int, string> AuthenticateStandAloneServiceJson(string userName, string password, string applicationId, string userId, string userApplicationId)
        {
            return this.AuthenticateStandAloneService(userName, password, applicationId, userId, userApplicationId);
        }
    }
}
