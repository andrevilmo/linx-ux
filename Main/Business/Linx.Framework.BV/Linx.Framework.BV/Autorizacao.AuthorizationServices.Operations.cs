using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
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
using System.Web.Security;
using System.Transactions;
using System.Reflection;
using System.ComponentModel.Composition;
using Linx.Framework.BV.TransacaoAutorizacao;
using Linx.Framework.BV.UsuarioAutorizacao;
using System.Configuration;

namespace Linx.Framework.BV.Autorizacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class AutorizacaoDomainService
    {
        [Import(typeof(IAuthenticateUserExtension))]
        public IAuthenticateUserExtension AuthenticateUserExtension;

        partial void OnCreate()
        {
            // Prefer bin\Extension\, fall back to bin\ (deploy often places the DLL only in bin).
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] extensionPaths = new[]
            {
                System.IO.Path.Combine(baseDir, @"bin\Extension\"),
                System.IO.Path.Combine(baseDir, @"bin\")
            };

            System.ComponentModel.Composition.Hosting.AggregateCatalog catalog = null;
            foreach (string extensionPath in extensionPaths)
            {
                catalog = AssemblyHelper.LoadUserExtension("Linx.Framework.BV.AuthenticateUserExtension.dll", 0, extensionPath);
                if (!catalog.IsNull() && catalog.Count() > 0)
                    break;
            }

            if (!catalog.IsNull() && catalog.Count() > 0)
            {
                try
                {
                    System.ComponentModel.Composition.Hosting.CompositionContainer container = new System.ComponentModel.Composition.Hosting.CompositionContainer(catalog);
                    container.ComposeParts(this);
                }
                catch { }
            }
        }

        [Invoke(HasSideEffects = true)]
        public LoginInfo AuthenticateUser(string authenticatedUser, Guid applicationUid, Guid companyUid, Guid accessGroupUid, int environmentId)
        {
            LoginInfo loginInfo = new LoginInfo();
            try
            {
                AutorizacaoDomainService dsAutorizacao = new AutorizacaoDomainService();
                TcsUsuarioAcesso usuarioAcesso = (from result in dsAutorizacao.GetTcsUsuarioAcessoNoAssociations().Where(i => i.NomeAutenticacao.ToUpper() == authenticatedUser.ToUpper() && i.UidAplicacao == applicationUid &&
                                                      i.UidEmpresa == companyUid && i.IdTcsAmbiente == environmentId)
                                                  select result).FirstOrDefault();

                if (!usuarioAcesso.IsNull())
                {
                    //Cache Id Linx 
                    this.UpdateCompanyInfo(companyUid);

                    //Cache Id Grupo Economico
                    this.UpdateCompanyInfo(usuarioAcesso.UidGrupoEconomico);

                    //Validate User (Inativo - Vig�ncia)
                    this.ValidateUserAccess(usuarioAcesso.UidUsuario);

                    loginInfo = new LoginInfo()
                    {
                        UidUsuario = usuarioAcesso.UidUsuario,
                        IdUsuario = usuarioAcesso.IdUsuario,
                        NomeUsuario = usuarioAcesso.NomeUsuario,
                        NomeCurtoUsuario = usuarioAcesso.NomeCurtoUsuario,
                        AutenticacaoWindows = usuarioAcesso.AutenticacaoWindows,
                        DataExpiracaoSenha = usuarioAcesso.DataExpiracaoSenha,
                        Ambientes = new List<AmbienteInfo>(),
                        UidGrupoEconomico = usuarioAcesso.UidGrupoEconomico,
                        DescricaoGrupoEconomico = usuarioAcesso.DescGrupoEconomico,
                        IdLinxGrupoEconomico = usuarioAcesso.IdLinxGrupoEconomico
                    };

                    //Lista de Grupos Econ�micos
                    int[] gpeconList = BusinessUserServiceHelper.GetCurrentUserGpeconInfo(new Dictionary<string, string> { { "CurrentUser", usuarioAcesso.UidUsuario.ToString() }, { "EconomicGroup", usuarioAcesso.UidGrupoEconomico.ToString() }, { "Environment", environmentId.ToString() }, { "CurrentCompany", companyUid.ToString() } });
                    Empresa.EmpresaDomainService dsEmpresa = new Empresa.EmpresaDomainService();
                    loginInfo.GruposEconomicos = dsEmpresa.GetTcsEmpresaAutenticacaoNoAssociations().Where(i => gpeconList.Contains(i.IdLinx)).Select(i => new GpeconInfo() { IdGpecon = i.IdLinx, Descricao = i.NomeEmpresa }).ToList();

                    //Token
                    Guid token = this.UpdateToken(usuarioAcesso.UidUsuario, applicationUid, companyUid, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.Administrador, usuarioAcesso.MultiGpecon, usuarioAcesso.IdTcsAmbienteRelacionado);

                    loginInfo.Ambientes.Add(new AmbienteInfo()
                    {
                        IdTcsAmbiente = usuarioAcesso.IdTcsAmbiente,
                        DescricaoAmbiente = usuarioAcesso.DescAmbiente,
                        IdTcsAplicativo = usuarioAcesso.IdTcsAplicativo,
                        DescricaoAplicativo = usuarioAcesso.DescAplicativo,
                        UrlAplicativo = Utils.ChangeSpecialCharacters(usuarioAcesso.DescAplicativo),
                        Token = token,
                        UidAplicacao = applicationUid,
                        UidEmpresa = companyUid,
                        DescricaoEmpresa = usuarioAcesso.DescEmpresa,
                        IndicaAdministrador = usuarioAcesso.Administrador,
                        UrlServiceBus = GetServiceBusUrl(usuarioAcesso.IdTcsAmbiente),
                        IndicaMultiGpecon = (usuarioAcesso.MultiGpecon && (usuarioAcesso.IdLinxAmbiente == usuarioAcesso.IdLinxGrupoEconomico))
                    });

                    if (!usuarioAcesso.IdTcsAmbienteRelacionado.IsNullOrEmpty())
                    {
                        loginInfo.Ambientes.Add(LogonAmbienteRelacionado(usuarioAcesso.IdUsuario, usuarioAcesso.UidUsuario, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.IdTcsAmbienteRelacionado.Value));
                    }
                    this.LogAuthAccessSuccess(authenticatedUser, "Application");
                    return loginInfo;
                }
                else
                {
                    this.LogAuthAccessFailure(authenticatedUser, ErrorConstants._ApplicationAccessDenied);
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));
                }
            }
            catch (LicenseException licenseError)
            {
                throw licenseError;
            }
            catch (Exception oException)
            {
                string errorMessage = oException.InnerException.IsNull() ? oException.Message : oException.InnerException.Message;
                throw new DomainException(errorMessage);
            }
        }

        private AmbienteInfo LogonAmbienteRelacionado(Int64 idUsuario, Guid uidUsuario, int idAmbiente, int idAmbienteRelacionado)
        {
            var acessoRelacionado = (from result in this.DbContext.TCS_USUARIO_ACESSO
                                     where result.TCS_USUARIO_AUTENTICACAO.ID_USUARIO == idUsuario && result.ID_TCS_AMBIENTE == idAmbienteRelacionado
                                     select new
                                     {
                                         UidEmpresa = result.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                         UidAplicacao = result.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO,
                                         IdTcsAplicativo = result.TCS_AMBIENTE.TCS_APLICACAO.ID_TCS_APLICATIVO,
                                         DescricaoAmbiente = result.TCS_AMBIENTE.DESCRICAO_AMBIENTE,
                                         DescricaoAplicativo = result.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO,
                                         DescricaoEmpresa = result.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA,
                                         Administrador = result.INDICA_ADMINISTRADOR,
                                         MultiGpecon = result.INDICA_MULTI_GPECON
                                     }).FirstOrDefault();

            if (acessoRelacionado.IsNull())
                throw new Exception(String.Format("{0} - {1}", ErrorConstants._InvalidRelatedEnvironmentInfo.Code, ErrorConstants._InvalidRelatedEnvironmentInfo.Message));

            //Cache Id Empresa Relacionada
            this.UpdateCompanyInfo(acessoRelacionado.UidEmpresa);

            Guid token = this.UpdateToken(uidUsuario, acessoRelacionado.UidAplicacao, acessoRelacionado.UidEmpresa, idAmbienteRelacionado, acessoRelacionado.Administrador, acessoRelacionado.MultiGpecon, idAmbiente);

            return new AmbienteInfo()
            {
                IdTcsAmbiente = idAmbienteRelacionado,
                DescricaoAmbiente = acessoRelacionado.DescricaoAmbiente,
                IdTcsAplicativo = acessoRelacionado.IdTcsAplicativo,
                DescricaoAplicativo = acessoRelacionado.DescricaoAplicativo,
                UrlAplicativo = Utils.ChangeSpecialCharacters(acessoRelacionado.DescricaoAplicativo),
                Token = token,
                UidAplicacao = acessoRelacionado.UidAplicacao,
                UidEmpresa = acessoRelacionado.UidEmpresa,
                DescricaoEmpresa = acessoRelacionado.DescricaoEmpresa,
                IndicaAdministrador = acessoRelacionado.Administrador,
                UrlServiceBus = GetServiceBusUrl(idAmbienteRelacionado),
                IndicaMultiGpecon = acessoRelacionado.MultiGpecon
            };
        }

        [Invoke(HasSideEffects = true)]
        public bool ValidateToken(Guid userUid, Guid authorizationToken, Guid applicationUid, Guid companyUid, Guid accessGroupUid, int environmentId)
        {
            List<Acesso> TokenList = WebCacheHelper.GetWebCache<List<Acesso>>(userUid.ToString());

            //Se n�o encontrou informa��es de Login no Cache
            if (TokenList.IsNull())
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._CacheInfoNotFound.Code, ErrorConstants._CacheInfoNotFound.Message));

            if (!BusinessUserServiceHelper.HasAppTokenControl())
                return true;
            else
            {
                Object appCache = WebCacheHelper.GetWebCache("AllowedApplications");

                List<Guid> allowedApps;

                //aqui

                //if (appCache.IsNull())
                //{
                //    var query = from result in this.DbContext.TCS_APLICACAO
                //                where result.ID_APLICATIVO == 15
                //                select result.UID_APLICACAO;

                //    allowedApps = query.ToList();
                //    WebCacheHelper.UpdateWebCache("AllowedApplications", allowedApps);
                //}
                //else
                //{
                //    allowedApps = appCache as List<Guid>;
                //}

                //if (allowedApps.Where(i => i == applicationUid).Count() > 0)
                //    return true;

                Acesso acesso = TokenList.Where(i => i.IdTcsAmbiente == environmentId && i.Token == authorizationToken).FirstOrDefault();

                if (acesso.IsNull())
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._AuthorizationTokenExpired.Code, ErrorConstants._AuthorizationTokenExpired.Message));

                //Validates Tcs_Ambiente
                Object cache = UpdateEnvironmentInfo(environmentId);
                string[] tcsAmbienteInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                if (tcsAmbienteInfo[0] != applicationUid.ToString() || tcsAmbienteInfo[1] != companyUid.ToString())
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._InvalidEnvironmentInfo.Code, ErrorConstants._InvalidEnvironmentInfo.Message));

                return true;
            }
        }

        [Invoke(HasSideEffects = true)]
        public Dictionary<int, string> AuthenticatePos(string userName, string password, string applicationId)
        {
            try
            {
                Guid applicationUid = Guid.Parse(applicationId);

                if (!this.ValidateUser(userName, password))
                {
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));
                }

                bool isDefaultAccess = applicationUid == Guid.Empty;
                string dynQuery = "";

                if (isDefaultAccess)
                    dynQuery = " it.INDICA_ACESSO_PADRAO == true";
                else
                    dynQuery = "it.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO == Guid'" + applicationUid + "'";

                var usuarioAcesso = (from result in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery)
                                     where result.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO.ToUpper() == userName.ToUpper()
                                     orderby result.INDICA_ACESSO_PADRAO descending
                                     select new
                                     {
                                         UidEmpresa = result.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                         IdLinxEmpresa = result.TCS_AMBIENTE.ID_LINX,
                                         UidUsuario = result.TCS_USUARIO_AUTENTICACAO.UID_USUARIO,
                                         IdUsuario = result.ID_USUARIO,
                                         UidGrupoEconomico = result.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                         IdLinxGpecon = result.TCS_USUARIO_AUTENTICACAO.ID_LINX_GPECON,
                                         IdTcsAmbiente = result.ID_TCS_AMBIENTE,
                                         Administrador = result.INDICA_ADMINISTRADOR,
                                         MultiGpecon = result.INDICA_MULTI_GPECON,
                                         IdTcsAmbienteRelacionado = result.ID_TCS_AMBIENTE_RELACIONADO,
                                         UidAplicacao = result.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO
                                     }).FirstOrDefault();

                if (usuarioAcesso.IsNull())
                {
                    if (isDefaultAccess)
                    {
                        this.LogAuthAccessFailure(userName, ErrorConstants._UserHasNoDefaultAccess);
                        throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserHasNoDefaultAccess.Code, ErrorConstants._UserHasNoDefaultAccess.Message));
                    }
                    else
                    {
                        this.LogAuthAccessFailure(userName, ErrorConstants._ApplicationAccessDenied);
                        throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));
                    }
                }
                //Validate User (Inativo - Vig�ncia)
                this.ValidateUserAccess(usuarioAcesso.UidUsuario);

                //Acesso relacionado
                if (!usuarioAcesso.IdTcsAmbienteRelacionado.IsNullOrEmpty())
                {
                    LogonAmbienteRelacionado(usuarioAcesso.IdUsuario, usuarioAcesso.UidUsuario, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.IdTcsAmbienteRelacionado.Value);
                }
                var authInfoPos = this.AuthenticationInfo(usuarioAcesso.UidEmpresa, usuarioAcesso.UidUsuario, usuarioAcesso.UidGrupoEconomico, usuarioAcesso.UidAplicacao, usuarioAcesso.IdLinxEmpresa, usuarioAcesso.IdLinxGpecon, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.Administrador, usuarioAcesso.MultiGpecon, usuarioAcesso.IdTcsAmbienteRelacionado, usuarioAcesso.IdUsuario);
                this.LogAuthAccessSuccess(userName, "POS");
                return authInfoPos;
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }

        [Invoke(HasSideEffects = true)]
        private Guid UpdateToken(Guid userUid, Guid applicationUid, Guid companyUid, int environmentId, bool isAdministrator, bool isMultiGpecon, int? idAmbienteRelacionado)
        {
            //Controle de Licen�a por Usu�rio
            UserInfo userInfo = ValidateUserAccess(userUid, true);
            int idLinxEnvironment = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment(companyUid).GetValueOrDefault();

            try
            {
                LicenseControl.Validate(userInfo.NomeAutenticacao, userInfo.NomeUsuario, companyUid);
            }
            catch (Exception oException)
            {
                string errorMessage = string.Format("{0} Ambiente : {1} | Id Linx : {2} |  Usu�rio: {3}.", oException.Message, BusinessUserServiceHelper.GetEnvironmentName(environmentId), idLinxEnvironment, userInfo.NomeAutenticacao);
                throw new Linx.Framework.BV.LicenseException(errorMessage);
            }

            List<Acesso> TokenList = WebCacheHelper.GetWebCache<List<Acesso>>(userUid.ToString());
            Guid token = Guid.NewGuid();
            if (TokenList.IsNull())
            {
                TokenList = new List<Acesso>
                {
                    new Acesso() { IdTcsAmbiente = environmentId, Token = token, IndicaAdministrador = isAdministrator, IndicaMultiGpecon = isMultiGpecon, IdAmbienteRelacionado = idAmbienteRelacionado }
                };
                WebCacheHelper.UpdateWebCache(userUid.ToString(), TokenList, 12);
            }
            else
            {
                Acesso acesso = TokenList.Where(i => i.IdTcsAmbiente == environmentId).FirstOrDefault();

                if (acesso.IsNull())
                    TokenList.Add(new Acesso() { IdTcsAmbiente = environmentId, Token = token, IndicaAdministrador = isAdministrator, IndicaMultiGpecon = isMultiGpecon, IdAmbienteRelacionado = idAmbienteRelacionado });
                else
                {
                    acesso.Token = token;
                    acesso.IndicaAdministrador = isAdministrator;
                    acesso.IndicaMultiGpecon = isMultiGpecon;
                    acesso.IdAmbienteRelacionado = idAmbienteRelacionado;
                }

                WebCacheHelper.UpdateWebCache(userUid.ToString(), TokenList, 12);
            }

            //Cache Tcs_Ambiente
            UpdateEnvironmentInfo(environmentId);

            return token;
        }

        [Invoke(HasSideEffects = false)]
        public Dictionary<int, string> AuthenticateJson(string userName, string password, string applicationId)
        {
            return this.AuthenticatePos(userName, password, applicationId);
        }

        [Invoke(HasSideEffects = true)]
        protected internal static string EmailBody(string userName, string authenticationName, string password)
        {
            try
            {
                string file = System.Web.Hosting.HostingEnvironment.MapPath("~/bin/LinxMail/SendPasswordMailBody.html");

                if (!System.IO.File.Exists(file))
                    throw new Exception("Arquivo 'SendPasswordMailBody.html' n�o encontrado.");

                var webClient = new System.Net.WebClient();
                string successHtml = webClient.DownloadString(file);
                successHtml = successHtml.Replace("{0}", userName);
                successHtml = successHtml.Replace("{1}", authenticationName);
                successHtml = successHtml.Replace("{2}", password);
                return successHtml;
            }
            catch (Exception oException)
            {
                string message = string.Format("Erro ao gerar corpo do email para envio de senha. {0}.", oException.Message);
                throw new Exception(message);
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
                UsuarioAutorizacao.UsuarioAutorizacaoDomainService dsUsuarioAut = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();

                TcsUsuarioAutenticacao usuario = dsUsuarioAut.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.UidUsuario == userUid).FirstOrDefault();

                if (usuario.IsNullOrEmpty())
                {
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
                }

                //Validates user and current password.
                if (!this.ValidateUser(usuario.NomeAutenticacao, oldPassword))
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

                    int expirationDays = ResolvePasswordExpirationDays();

                    TcsUsuarioAutenticacao usuarioOld = new TcsUsuarioAutenticacao();
                    usuarioOld.CopyInstanceFrom(usuario);
                    usuario.DataExpiracaoSenha = DateTime.Now.Date.AddDays(expirationDays);
                    dsUsuarioAut.AddCustomChanges(usuario, usuarioOld, ChangeOperation.Update);
                    dsUsuarioAut.SaveCustomChanges();
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
            UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            TcsUsuarioAutenticacao usuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName).FirstOrDefault();

            if (usuario.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            if (usuario.AutenticacaoWindows)
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserHasWindowsAuthentication.Code, ErrorConstants._UserHasWindowsAuthentication.Message));
            }

            MembershipUser user = Membership.GetUser(userName);

            if (user.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            using (TransactionScope transaction = new TransactionScope())
            {
                string password = user.ResetPassword("Dog");

                if (usuario.IsNullOrEmpty())
                {
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
                }

                TcsUsuarioAutenticacao usuarioOld = new TcsUsuarioAutenticacao();
                usuarioOld.CopyInstanceFrom(usuario);

                usuario.DataExpiracaoSenha = DateTime.Now.Date;
                ds.AddCustomChanges(usuario, usuarioOld, ChangeOperation.Update);
                ds.SaveCustomChanges();

                Linx.Tools.LinxMail.Send(usuario.Email, "Recupera��o de senha de usu�rio.".Translate(), true, AutorizacaoDomainService.EmailBody(usuario.NomeUsuario, usuario.NomeAutenticacao, password));

                transaction.Complete();
            }

            return true;
        }

        [Invoke(HasSideEffects = false)]
        public bool RecoverUserPasswordJson(string userName)
        {
            return this.RecoverUserPassword(userName);
        }

        /// <summary>
        /// Tempo de validade (em minutos) do link de redefini??????o de senha.
        /// </summary>
        private const int _PasswordResetTokenValidityMinutes = 60;

        /// <summary>
        /// Gera um token de redefini??????o de senha e envia, por e-mail, um link para o usu???rio redefinir a senha.
        /// </summary>
        /// <param name="userName">Nome de autentica??????o do usu???rio.</param>
        /// <param name="callbackUrl">URL base da aplica??????o que hospeda a p???gina de redefini??????o de senha.</param>
        [Invoke(HasSideEffects = true)]
        public bool SendPasswordResetLink(string userName, string callbackUrl)
        {
            UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            TcsUsuarioAutenticacao usuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName).FirstOrDefault();

            if (usuario.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            if (usuario.AutenticacaoWindows)
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserHasWindowsAuthentication.Code, ErrorConstants._UserHasWindowsAuthentication.Message));
            }

            MembershipUser user = Membership.GetUser(userName);

            if (user.IsNullOrEmpty())
            {
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
            }

            string token = GeneratePasswordResetToken(userName, user);

            // O callbackUrl ??? a URL completa da p???gina que tratar??? a redefini??????o (ex.: p???gina de login do Portal
            // ou a p???gina ResetPassword da aplica??????o). O token ??? anexado como par???metro de query string.
            string separator = (!callbackUrl.IsNullOrEmpty() && callbackUrl.IndexOf('?') >= 0) ? "&" : "?";
            string link = string.Format("{0}{1}token={2}", callbackUrl, separator, System.Web.HttpUtility.UrlEncode(token));

            Linx.Tools.LinxMail.Send(usuario.Email, "Redefinição de senha de usuário.".Translate(), true, ResetPasswordEmailBody(usuario.NomeUsuario, link));

            return true;
        }

        /// <summary>
        /// Valida um token de redefini??????o de senha (sem efetuar a troca).
        /// </summary>
        [Invoke(HasSideEffects = false)]
        public bool ValidatePasswordResetToken(string token)
        {
            string userName;
            return TryValidatePasswordResetToken(token, out userName);
        }

        /// <summary>
        /// Redefine a senha do usu???rio a partir de um token v???lido recebido por e-mail.
        /// </summary>
        [Invoke(HasSideEffects = true)]
        public bool ResetPasswordWithToken(string token, string newPassword)
        {
            try
            {
                string userName;
                if (!TryValidatePasswordResetToken(token, out userName))
                {
                    throw new DomainException("Link de redefinição de senha inválido ou expirado.".Translate());
                }

                UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
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

                // Keep Membership outside TransactionScope: pairing it with EF often promotes to MSDTC
                // and fails on QA with "An error occurred while executing the command definition."
                if (user.IsLockedOut && !user.UnlockUser())
                    throw new DomainException(ErrorConstants.FormatUserLockedOutMessage());

                user = Membership.GetUser(userName);
                bool passwordChanged = user.ChangePassword(user.ResetPassword("Dog"), newPassword);
                if (!passwordChanged)
                {
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._ChangePasswordError.Code, ErrorConstants._ChangePasswordError.Message));
                }

                int expirationDays = ResolvePasswordExpirationDays();
                TcsUsuarioAutenticacao usuarioOld = new TcsUsuarioAutenticacao();
                usuarioOld.CopyInstanceFrom(usuario);
                usuario.DataExpiracaoSenha = DateTime.Now.Date.AddDays(expirationDays);
                ds.AddCustomChanges(usuario, usuarioOld, ChangeOperation.Update);
                ds.SaveCustomChanges();

                return true;
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception oException)
            {
                string detail = oException.Message;
                if (oException.InnerException != null && !oException.InnerException.Message.IsNullOrEmpty())
                    detail = detail + "\n" + oException.InnerException.Message;
                if (oException.InnerException != null && oException.InnerException.InnerException != null
                    && !oException.InnerException.InnerException.Message.IsNullOrEmpty())
                    detail = detail + "\n" + oException.InnerException.InnerException.Message;
                throw new DomainException(ErrorConstants.EnsureUserLockedOutMessage(detail));
            }
        }

        /// <summary>
        /// Reads DIAS_EXPIRACAO_SENHA_USUARIO when ControleSistema context is available;
        /// falls back to 90 days for anonymous flows (e.g. email password-reset link).
        /// </summary>
        private static int ResolvePasswordExpirationDays()
        {
            const int defaultExpirationDays = 90;
            try
            {
                var parameterValue = LinxBusinessParameters.GetParameter<string>("DIAS_EXPIRACAO_SENHA_USUARIO", null);
                if (!parameterValue.IsNullOrEmpty())
                    return Int32.Parse(parameterValue);
            }
            catch
            {
                // Portal reset has no authenticated EconomicGroup/ControleSistema connection.
            }
            return defaultExpirationDays;
        }

        private static string GeneratePasswordResetToken(string userName, MembershipUser user)
        {
            // Token autossuficiente (n???o exige tabela de tokens): cont???m usu???rio, validade e a data da
            // ???ltima troca de senha. Como a data muda ap???s a redefini??????o, o token deixa de ser v???lido (uso ???nico).
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

            // Uso ???nico: o token s??? ??? v???lido enquanto a senha n???o tiver sido alterada ap???s a sua emiss???o.
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
            //aqui
            TransacaoAutorizacao.TransacaoAutorizacaoDomainService ds = new TransacaoAutorizacaoDomainService();
            List<string> boList = Utils.GetObjectClassName(boName);
            return ds.GetTcsTransacaoAutorizacaoNoAssociations().Where(i => !i.Inativo && (boList.Contains(i.ObjetoClasseNome) || i.ClasseNome == transaction));
        }

        [Invoke(HasSideEffects = true)]
        public Dictionary<int, string> AuthenticateStandAloneService(string userName, string password, string applicationId, string userId, string userApplicationId)
        {
            //aqui
            try
            {
                //1 -> CurrentCompany
                //2 -> AuthorizationToken
                //3 -> CurrentUser
                //4 -> AccessGroup
                //5 -> EconomicGroup
                //6 -> Environment
                Dictionary<int, string> currentInfo = new Dictionary<int, string>();

                int[] allowedApplications = { 14, 15 };

                if (!this.ValidateUser(userName, password))
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));

                Guid uidServerApplication = Guid.Parse(applicationId);
                Guid uidUserApplication = Guid.Parse(userApplicationId);
                Guid uidUsuario = Guid.Parse(userId);

                var appAccess = (from result in this.DbContext.TCS_USUARIO_ACESSO
                                 where result.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO.ToUpper() == userName.ToUpper() && result.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO == uidServerApplication
                                 select new { UidUsuario = result.TCS_USUARIO_AUTENTICACAO.UID_USUARIO, IdTcsAplicativo = result.TCS_AMBIENTE.TCS_APLICACAO.ID_TCS_APLICATIVO }).FirstOrDefault();

                //aqui
                //if (appAccess.IsNull() || !allowedApplications.Contains(appAccess.IdAplicativo))
                //    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));

                //Validate User (Inativo - Vig�ncia)
                this.ValidateUserAccess(appAccess.UidUsuario);

                var userAccess = (from result in this.DbContext.TCS_USUARIO_ACESSO
                                  where result.TCS_USUARIO_AUTENTICACAO.UID_USUARIO == uidUsuario
                                  select new
                                  {
                                      Uidempresa = result.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                      IdLinxEmpresa = result.TCS_AMBIENTE.ID_LINX,
                                      UidUsuario = result.TCS_USUARIO_AUTENTICACAO.UID_USUARIO,
                                      IdUsuario = result.ID_USUARIO,
                                      UidGrupoEconomico = result.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                      IdLinxGpecon = result.TCS_USUARIO_AUTENTICACAO.ID_LINX_GPECON,
                                      IdTcsAmbiente = result.ID_TCS_AMBIENTE,
                                      Administrador = result.INDICA_ADMINISTRADOR,
                                      MultiGpecon = result.INDICA_MULTI_GPECON,
                                      IdAmbienteRelacionado = result.ID_TCS_AMBIENTE_RELACIONADO
                                  }).FirstOrDefault();

                if (userAccess.IsNull())
                {
                    this.LogAuthAccessFailure(userName, ErrorConstants._ApplicationAccessDenied);
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));
                }

                //Validate User (Inativo - Vig�ncia)
                this.ValidateUserAccess(userAccess.UidUsuario);

                var authInfoSA = this.AuthenticationInfo(userAccess.Uidempresa, userAccess.UidUsuario, userAccess.UidGrupoEconomico, uidUserApplication, userAccess.IdLinxEmpresa, userAccess.IdLinxGpecon, userAccess.IdTcsAmbiente, userAccess.Administrador, userAccess.MultiGpecon, userAccess.IdAmbienteRelacionado, userAccess.IdUsuario);
                this.LogAuthAccessSuccess(userName, "StandAlone");
                return authInfoSA;
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }

        [Invoke(HasSideEffects = false)]
        public Dictionary<int, string> AuthenticateStandAloneServiceJson(string userName, string password, string applicationId, string userId, string userApplicationId)
        {
            return this.AuthenticateStandAloneService(userName, password, applicationId, userId, userApplicationId);
        }

        [Invoke(HasSideEffects = true)]
        public bool ValidateUser(string userName, string userPassword)
        {
            // Always surface lockout before attempting password (Membership returns false for locked users).
            this.ThrowIfMembershipUserLockedOut(userName);

            bool authenticated;
            if (!AuthenticateUserExtension.IsNull())
                authenticated = AuthenticateUserExtension.ValidateUserExtension(userName, userPassword);
            else
                authenticated = Membership.ValidateUser(userName, userPassword);

            if (!authenticated)
            {
                // Audit failed attempt (Membership may have just locked the account).
                try { this.LogAuthAccessFailure(userName, ErrorConstants._UserBadNameOrPassword); } catch { }
                this.ThrowIfMembershipUserLockedOut(userName);
            }

            return authenticated;
        }

        /// <summary>
        /// Throws ERRAUT020 when the ASP.NET Membership account is locked out.
        /// </summary>
        private void ThrowIfMembershipUserLockedOut(string userName)
        {
            if (this.IsMembershipUserLockedOut(userName))
            {
                try { this.LogAuthAccessFailure(userName, ErrorConstants._UserLockedOut.Code, ErrorConstants._UserLockedOut.Message, null, false); } catch { }
                throw new DomainException(ErrorConstants.FormatUserLockedOutMessage());
            }
        }

        /// <summary>
        /// Returns whether the ASP.NET Membership account is locked out after invalid password attempts.
        /// </summary>
        [Invoke(HasSideEffects = false)]
        public bool IsMembershipUserLockedOut(string userName)
        {
            if (userName.IsNullOrEmpty())
                return false;

            try
            {
                string membershipUserName = userName;

                // Prefer the canonical auth name from TCS (matches aspnet_Users.UserName).
                try
                {
                    UsuarioAutorizacao.UsuarioAutorizacaoDomainService dsUsuario = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
                    string canonical = (from result in dsUsuario.GetTcsUsuarioAutenticacaoNoAssociations()
                                        where result.NomeAutenticacao.ToUpper() == userName.ToUpper()
                                        select result.NomeAutenticacao).FirstOrDefault();
                    if (!canonical.IsNullOrEmpty())
                        membershipUserName = canonical;
                }
                catch { }

                MembershipUser user = Membership.GetUser(membershipUserName, false);
                if (user.IsNull() && !Membership.Provider.IsNull())
                    user = Membership.Provider.GetUser(membershipUserName, false);

                if (user.IsNull() && !Membership.Provider.IsNull())
                {
                    int totalRecords;
                    MembershipUserCollection matches = Membership.Provider.FindUsersByName(membershipUserName, 0, 1, out totalRecords);
                    if (!matches.IsNull() && matches.Count > 0)
                        user = matches.Cast<MembershipUser>().FirstOrDefault();
                }

                return !user.IsNull() && user.IsLockedOut;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Unlocks an ASP.NET Membership account previously locked by invalid password attempts.
        /// </summary>
        [Invoke(HasSideEffects = true)]
        public bool UnlockMembershipUser(string userName)
        {
            if (userName.IsNullOrEmpty())
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));

            MembershipUser user = Membership.GetUser(userName, false);
            if (user.IsNullOrEmpty() && !Membership.Provider.IsNull())
                user = Membership.Provider.GetUser(userName, false);

            if (user.IsNullOrEmpty())
                throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));

            if (!user.IsLockedOut)
                return true;

            if (!user.UnlockUser())
                throw new DomainException("Nao foi possivel desbloquear o usuario.".Translate());

            return true;
        }

        [Invoke(HasSideEffects = true)]
        protected internal object UpdateCompanyInfo(Guid companyUid)
        {
            string cache = WebCacheHelper.GetWebCache<string>(companyUid.ToString());
            if (cache.IsNull() || cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None).Count() < 3)
            {
                var companyInfo = (from result in this.DbContext.TCS_EMPRESA_AUTENTICACAO where result.UID_EMPRESA == companyUid select new { UidEmpresa = result.UID_EMPRESA, IdLinx = result.ID_LINX, NomeEmpresa = result.NOME_EMPRESA, Cnpj = result.CNPJ_CPF }).FirstOrDefault();

                if (companyInfo.IsNull())
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._InvalidCompanyInfo.Code, ErrorConstants._InvalidCompanyInfo.Message));

                cache = string.Format("{0}|{1}|{2}", companyInfo.IdLinx, companyInfo.NomeEmpresa, companyInfo.Cnpj);
                WebCacheHelper.UpdateWebCache(companyInfo.UidEmpresa.ToString(), cache, 720);
            }
            return cache;
        }

        [Invoke(HasSideEffects = true)]
        protected internal object UpdateEnvironmentInfo(int environmentId)
        {
            string cacheKey = String.Format("EnvironmentInfo_{0}", environmentId.ToString());
            string cache = WebCacheHelper.GetWebCache<string>(cacheKey);

            if (cache.IsNull())
            {
                var tcsAmbiente = (from result in this.DbContext.TCS_AMBIENTE
                                   where result.ID_TCS_AMBIENTE == environmentId
                                   select new
                                   {
                                       UidAplicacao = result.TCS_APLICACAO.UID_APLICACAO,
                                       UidEmpresa = result.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                       IdTcsAplicativo = result.TCS_APLICACAO.ID_TCS_APLICATIVO,
                                       DescAmbiente = result.DESCRICAO_AMBIENTE,
                                       DescAplicativo = result.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                                   }).FirstOrDefault();

                if (tcsAmbiente.IsNull())
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._InvalidEnvironmentInfo.Code, ErrorConstants._InvalidEnvironmentInfo.Message));

                WebCacheHelper.UpdateWebCache(cacheKey, string.Format("{0}|{1}|{2}|{3}|{4}", tcsAmbiente.UidAplicacao, tcsAmbiente.UidEmpresa, tcsAmbiente.DescAmbiente, tcsAmbiente.IdTcsAplicativo, tcsAmbiente.DescAplicativo), 720);
                cache = WebCacheHelper.GetWebCache<string>(cacheKey);
            }

            return cache;
        }

        private string GetServiceBusUrl(int idTcsAmbiente)
        {
            //Url barramento de Servi�o
            var serviceUrl = string.Empty;
            Ambiente.AmbienteDomainService ds = new Ambiente.AmbienteDomainService();
            var serviceInfo = ds.GetServicoExcecaoMultiEnvironment(new Ambiente.EnvironmentInfo[] { new Ambiente.EnvironmentInfo() { EnvironmentId = idTcsAmbiente } });
            if (!serviceInfo.IsNullOrEmpty())
            {
                serviceUrl = serviceInfo.Where(i => i.IdTcsAmbiente == idTcsAmbiente && i.Servico == "LinxServiceBus").Select(i => i.Url).FirstOrDefault();
            }
            return serviceUrl;
        }

        private Dictionary<int, string> AuthenticationInfo(Guid uidEmpresa, Guid uidUsuario, Guid uidGrupoEconomico, Guid uidAplicacao, int idLinxEmpresa, int idLinxGrupoEconomico, int idTcsAmbiente, bool administrador, bool multiGpecon, int? idAmbienteRelacionado, Int64 idUsuario)
        {
            //1 -> CurrentCompany
            //2 -> AuthorizationToken
            //3 -> CurrentUser
            //4 -> AccessGroup
            //5 -> EconomicGroup
            //6 -> Environment
            //7 -> CurrentUserId
            //8 -> ApplicationId
            //9 -> ServiceBusUrl
            Dictionary<int, string> currentInfo = new Dictionary<int, string>();

            //Cache Id Linx
            this.UpdateCompanyInfo(uidEmpresa);

            //Cache Id Grupo Economico
            this.UpdateCompanyInfo(uidGrupoEconomico);

            Guid token = this.UpdateToken(uidUsuario, uidAplicacao, uidEmpresa, idTcsAmbiente, administrador, multiGpecon, idAmbienteRelacionado);


            currentInfo.Add(1, uidEmpresa.ToString());
            currentInfo.Add(2, token.ToString());
            currentInfo.Add(3, uidUsuario.ToString());
            currentInfo.Add(4, Guid.Empty.ToString());
            currentInfo.Add(5, uidGrupoEconomico.ToString());
            currentInfo.Add(6, idTcsAmbiente.ToString());
            currentInfo.Add(7, idUsuario.ToString());
            currentInfo.Add(8, uidAplicacao.ToString());
            currentInfo.Add(9, GetServiceBusUrl(idTcsAmbiente));
            return currentInfo;
        }

        [Invoke(HasSideEffects = true)]
        public UserInfo ValidateUserAccess(Guid uidUsuario, bool updateOnly = false)
        {
            if (uidUsuario.IsNullOrEmpty())
            {
                return null;
            }

            DateTime currentDate = DateTime.Now.Date;
            string cacheKey = String.Format("UserInfo_{0}", uidUsuario.ToString());

            UserInfo usuario = WebCacheHelper.GetWebCache<UserInfo>(cacheKey);

            if (usuario.IsNull())
            {
                usuario = (from result in this.DbContext.TCS_USUARIO_AUTENTICACAO
                           where result.UID_USUARIO == uidUsuario
                           select new UserInfo()
                           {
                               UidUsuario = result.UID_USUARIO,
                               Inativo = result.INATIVO,
                               VigenciaInicial = result.VIGENCIA_INICIAL,
                               VigenciaFinal = result.VIGENCIA_FINAL,
                               IdUsuario = result.ID_USUARIO,
                               NomeAutenticacao = result.NOME_AUTENTICACAO,
                               NomeUsuario = result.NOME_USUARIO
                           }).FirstOrDefault();

                if (!usuario.IsNullOrEmpty())
                    WebCacheHelper.UpdateWebCache(cacheKey, usuario, 720); //30 dias
            }
            //else
            //    usuario = cache as UserInfo;

            if (!updateOnly)
            {

                if (usuario.IsNullOrEmpty())
                {
                    this.LogAuthAccessFailure(string.Empty, ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message, null, false);
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));
                }

                if (usuario.Inativo)
                {
                    this.LogAuthAccessFailure(usuario.NomeAutenticacao, ErrorConstants._UserNotActive);
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotActive.Code, ErrorConstants._UserNotActive.Message));
                }

                if (usuario.VigenciaInicial.Date > currentDate || usuario.VigenciaFinal.Date < currentDate)
                {
                    this.LogAuthAccessFailure(usuario.NomeAutenticacao, ErrorConstants._UserLoginExpired);
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserLoginExpired.Code, ErrorConstants._UserLoginExpired.Message));
                }
            }
            return usuario;
        }

        [Invoke(HasSideEffects = true)]
        public Dictionary<int, string> AuthenticateOData(string userName, string password, int environmentId)
        {
            try
            {
                if (!this.ValidateUser(userName, password))
                {
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));
                }

                var usuarioAcesso = (from result in this.DbContext.TCS_USUARIO_ACESSO
                                     where result.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO.ToUpper() == userName.ToUpper() && result.TCS_AMBIENTE.ID_TCS_AMBIENTE == environmentId
                                     select new
                                     {
                                         UidEmpresa = result.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                         IdLinxEmpresa = result.TCS_AMBIENTE.ID_LINX,
                                         UidUsuario = result.TCS_USUARIO_AUTENTICACAO.UID_USUARIO,
                                         IdUsuario = result.ID_USUARIO,
                                         UidGrupoEconomico = result.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA,
                                         IdLinxGpecon = result.TCS_USUARIO_AUTENTICACAO.ID_LINX_GPECON,
                                         IdTcsAmbiente = result.ID_TCS_AMBIENTE,
                                         Administrador = result.INDICA_ADMINISTRADOR,
                                         MultiGpecon = result.INDICA_MULTI_GPECON,
                                         UidAplicacao = result.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO,
                                         IdAmbienteRelacionado = result.ID_TCS_AMBIENTE_RELACIONADO
                                     }).FirstOrDefault();

                if (usuarioAcesso.IsNull())
                {
                    this.LogAuthAccessFailure(userName, ErrorConstants._ApplicationAccessDenied);
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));
                }

                //Validate User (Inativo - Vig�ncia)
                this.ValidateUserAccess(usuarioAcesso.UidUsuario);

                var authInfoOData = this.AuthenticationInfo(usuarioAcesso.UidEmpresa, usuarioAcesso.UidUsuario, usuarioAcesso.UidGrupoEconomico, usuarioAcesso.UidAplicacao, usuarioAcesso.IdLinxEmpresa, usuarioAcesso.IdLinxGpecon, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.Administrador, usuarioAcesso.MultiGpecon, usuarioAcesso.IdAmbienteRelacionado, usuarioAcesso.IdUsuario);
                this.LogAuthAccessSuccess(userName, "OData");
                return authInfoOData;
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }

    }
}
