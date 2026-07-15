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
            var catalog = AssemblyHelper.LoadUserExtension("Linx.Framework.BV.AuthenticateUserExtension.dll", 0, String.Format(@"{0}bin\Extension\", AppDomain.CurrentDomain.BaseDirectory));

            if (catalog.Count() > 0)
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

                    //Validate User (Inativo - Vigência)
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

                    //Lista de Grupos Econômicos
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
                    return loginInfo;
                }
                else
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));
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

            //Se não encontrou informações de Login no Cache
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
                        throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserHasNoDefaultAccess.Code, ErrorConstants._UserHasNoDefaultAccess.Message));
                    else
                        throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));
                }
                //Validate User (Inativo - Vigência)
                this.ValidateUserAccess(usuarioAcesso.UidUsuario);

                //Acesso relacionado
                if (!usuarioAcesso.IdTcsAmbienteRelacionado.IsNullOrEmpty())
                {
                    LogonAmbienteRelacionado(usuarioAcesso.IdUsuario, usuarioAcesso.UidUsuario, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.IdTcsAmbienteRelacionado.Value);
                }
                return this.AuthenticationInfo(usuarioAcesso.UidEmpresa, usuarioAcesso.UidUsuario, usuarioAcesso.UidGrupoEconomico, usuarioAcesso.UidAplicacao, usuarioAcesso.IdLinxEmpresa, usuarioAcesso.IdLinxGpecon, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.Administrador, usuarioAcesso.MultiGpecon, usuarioAcesso.IdTcsAmbienteRelacionado, usuarioAcesso.IdUsuario);
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }

        [Invoke(HasSideEffects = true)]
        private Guid UpdateToken(Guid userUid, Guid applicationUid, Guid companyUid, int environmentId, bool isAdministrator, bool isMultiGpecon, int? idAmbienteRelacionado)
        {
            //Controle de Licença por Usuário
            UserInfo userInfo = ValidateUserAccess(userUid, true);
            int idLinxEnvironment = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment(companyUid).GetValueOrDefault();

            try
            {
                LicenseControl.Validate(userInfo.NomeAutenticacao, userInfo.NomeUsuario, companyUid);
            }
            catch (Exception oException)
            {
                string errorMessage = string.Format("{0} Ambiente : {1} | Id Linx : {2} |  Usuário: {3}.", oException.Message, BusinessUserServiceHelper.GetEnvironmentName(environmentId), idLinxEnvironment, userInfo.NomeAutenticacao);
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
                    throw new Exception("Arquivo 'SendPasswordMailBody.html' não encontrado.");

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

                    int expirationDays = 90;
                    var parameterValue = LinxBusinessParameters.GetParameter<string>("DIAS_EXPIRACAO_SENHA_USUARIO", null);
                    if (!parameterValue.IsNullOrEmpty())
                        expirationDays = Int32.Parse(parameterValue);

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

                Linx.Tools.LinxMail.Send(usuario.Email, "Recuperação de senha de usuário.".Translate(), true, AutorizacaoDomainService.EmailBody(usuario.NomeUsuario, usuario.NomeAutenticacao, password));

                transaction.Complete();
            }

            return true;
        }

        [Invoke(HasSideEffects = false)]
        public bool RecoverUserPasswordJson(string userName)
        {
            return this.RecoverUserPassword(userName);
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

                //Validate User (Inativo - Vigência)
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
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));

                //Validate User (Inativo - Vigência)
                this.ValidateUserAccess(userAccess.UidUsuario);

                return this.AuthenticationInfo(userAccess.Uidempresa, userAccess.UidUsuario, userAccess.UidGrupoEconomico, uidUserApplication, userAccess.IdLinxEmpresa, userAccess.IdLinxGpecon, userAccess.IdTcsAmbiente, userAccess.Administrador, userAccess.MultiGpecon, userAccess.IdAmbienteRelacionado, userAccess.IdUsuario);
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
            if (!AuthenticateUserExtension.IsNull())
                return AuthenticateUserExtension.ValidateUserExtension(userName, userPassword);
            else
                return Membership.ValidateUser(userName, userPassword);
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
            //Url barramento de Serviço
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
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotFound.Code, ErrorConstants._UserNotFound.Message));

                if (usuario.Inativo)
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserNotActive.Code, ErrorConstants._UserNotActive.Message));

                if (usuario.VigenciaInicial.Date > currentDate || usuario.VigenciaFinal.Date < currentDate)
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._UserLoginExpired.Code, ErrorConstants._UserLoginExpired.Message));
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
                    throw new Exception(String.Format("{0} - {1}", ErrorConstants._ApplicationAccessDenied.Code, ErrorConstants._ApplicationAccessDenied.Message));

                //Validate User (Inativo - Vigência)
                this.ValidateUserAccess(usuarioAcesso.UidUsuario);

                return this.AuthenticationInfo(usuarioAcesso.UidEmpresa, usuarioAcesso.UidUsuario, usuarioAcesso.UidGrupoEconomico, usuarioAcesso.UidAplicacao, usuarioAcesso.IdLinxEmpresa, usuarioAcesso.IdLinxGpecon, usuarioAcesso.IdTcsAmbiente, usuarioAcesso.Administrador, usuarioAcesso.MultiGpecon, usuarioAcesso.IdAmbienteRelacionado, usuarioAcesso.IdUsuario);
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }

    }
}
