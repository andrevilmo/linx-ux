using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Linx.Tools;
using Linx.Framework.BV.Usuario;
using Linx.Framework.BV.Autorizacao;
using Linx.Framework.BV.Multimidia;
using System.ServiceModel.DomainServices.Server;
using Linx.Framework.BV.Filtro;

namespace Linx.Framework.BV
{
    public static class BusinessUserServiceHelper
    {
        public static Guid? GetCurrentUserUid()
        {
            return GetCurrentUserUid(null);
        }

        public static Guid? GetCurrentUserUid(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled && !LocalServiceBus.CurrentUser.IsNullOrEmpty())
            {
                return LocalServiceBus.CurrentUser;
            }
            else
            {
                string currentUser = ServiceHelper.GetMessageProperty("CurrentUser", headers);
                if (currentUser.IsNullOrEmpty())
                    return null;
                else
                    return Guid.Parse(currentUser);
            }
        }

        public static Int64? GetCurrentUserId()
        {
            return GetCurrentUserId(null);
        }

        public static Int64? GetCurrentUserId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUserId;
            else
            {
                UserInfo userInfo = GetUserInfo(headers);
                if (userInfo.IsNullOrEmpty())
                    return null;
                else
                    return GetUserInfo(headers).IdUsuario;
            }
        }

        public static bool IsUserMultiGpecon()
        {
            return IsUserMultiGpecon(null);
        }

        public static bool IsUserMultiGpecon(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.IsUserMultiGpecon;
            }
            else
            {
                if (GetCurrentUserUid(headers).IsNullOrEmpty())
                    return false;
                else
                {
                    Acesso acesso = GetAcesso(headers, false);
                    return acesso.IsNullOrEmpty() ? false : acesso.IndicaMultiGpecon;
                }
            }
        }

        public static bool IsUserAdministrator()
        {
            return IsUserAdministrator(null);
        }

        public static bool IsUserAdministrator(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IsUserAdministrator;
            else
                return GetAcesso(headers).IndicaAdministrador;
        }

        public static string GetCurrentLoginMode()
        {
            return GetCurrentLoginMode(null);
        }

        public static string GetCurrentLoginMode(Dictionary<string, string> headers)
        {
            string loginMode = ServiceHelper.GetMessageProperty("LoginMode", headers);
            if (loginMode.IsNullOrEmpty())
                return null;
            else
                return loginMode;

        }

        public static int? GetCurrentBranch()
        {
            return GetCurrentBranch(null);
        }

        public static int? GetCurrentBranch(Dictionary<string, string> headers)
        {
            string branch = ServiceHelper.GetMessageProperty("Branch", headers);

            if (branch.IsNullOrEmpty())
                return null;
            else
                return Convert.ToInt32(branch);
        }

        public static int[] GetCurrentUserBrandInfo(string connectionName, Dictionary<string, string> headers = null)
        {
            int? applicativeId = GetCurrentApplicativeId(headers);

            if ((connectionName.ToLower() == "connlinxoperacional" && applicativeId != 3 ) || (connectionName.ToLower() == "connlinxadministrativo" && applicativeId != 2))
            {
                headers = GetRelatedEnvironmentInfo(headers);
            }
            return GetCurrentUserBrandInfo(headers);
        }

        public static int[] GetCurrentUserBrandInfo(Dictionary<string, string> headers = null)
        {
            if (LocalServiceBus.Enabled)
                return new int[0];
            else
            {
                int? branch = GetCurrentBranch(headers);

                if (!branch.IsNull() && branch != -1)
                {
                    return new int[] { branch.Value };
                }

                string cacheKey = string.Format("BrandInfo_{0}_{1}", GetCurrentUserUid(headers).GetValueOrDefault(), GetCurrentEnvironmentId(headers).GetValueOrDefault().ToString());
                int[] brandInfo = WebCacheHelper.GetWebCache<int[]>(cacheKey);

                if (brandInfo.IsNull())
                {
                    Int64 idUsuario = GetCurrentUserId(headers).GetValueOrDefault();
                    Transacao.TransacaoDomainService dsTransacao = new Transacao.TransacaoDomainService(headers);
                    List<Int64> tcsPerfil = dsTransacao.GetTcsPerfilUsuario(idUsuario, headers);

                    Perfil.PerfilDomainService dsPerfil = new Perfil.PerfilDomainService(headers);
                    int[] filialPerfil = dsPerfil.GetTcsPerfilFilialNoAssociations().Where(i => tcsPerfil.Contains(i.IdPerfil)).Select(i => i.IdFilialPfj).Distinct().ToArray();

                    Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService(headers);
                    int[] filialUsuario = dsUsuario.GetTcsUsuarioFilialNoAssociations().Where(i => i.IdUsuario == idUsuario).Select(i => i.IdFilialPfj).Distinct().ToArray();

                    brandInfo = filialPerfil.Union(filialUsuario).Distinct().ToArray();
                    WebCacheHelper.UpdateWebCache(cacheKey, brandInfo, 720);
                }
                return brandInfo;
            }
        }

        public static int[] GetCurrentUserGpeconInfo(Dictionary<string, string> headers = null)
        {
            if (LocalServiceBus.Enabled)
            {
                return new int[] { LocalServiceBus.IdGpecon };
            }

            string cacheKey = string.Format("GpeconInfo_{0}", GetCurrentUserUid(headers).GetValueOrDefault());
            int[] gpeconList = WebCacheHelper.GetWebCache<int[]>(cacheKey);

            if (gpeconList.IsNull())
            {
                long idUsuario = GetCurrentUserId(headers).GetValueOrDefault();
                gpeconList = new int[] { GetCurrentIdGpecon(headers).GetValueOrDefault() };

                var parameterValue = LinxBusinessParameters.GetParameter<string>("PERMITE_MULTI_GPECON_USUARIO", new Dictionary<string, string>(), headers);

                if (!parameterValue.IsNullOrEmpty() && parameterValue.ToLower() == "true")
                {
                    UsuarioAutorizacao.UsuarioAutorizacaoDomainService dsUsuarioA = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
                    gpeconList = gpeconList.Union(dsUsuarioA.GetTcsUsuarioGpeconNoAssociations().Where(i => i.IdUsuario == idUsuario).Select(i => i.IdLinx)).Distinct().ToArray();
                }

                WebCacheHelper.UpdateWebCache(cacheKey, gpeconList, 720);
            }
            return gpeconList;
        }

        private static UserInfo GetUserInfo(Dictionary<string, string> headers)
        {
            Autorizacao.AutorizacaoDomainService ds = new AutorizacaoDomainService();
            Guid userUid = GetCurrentUserUid(headers).GetValueOrDefault();
            return ds.ValidateUserAccess(userUid, true);
        }

        private static Acesso GetAcesso(Dictionary<string, string> headers, bool showMessages = true)
        {
            string currentUser = GetCurrentUserUid(headers).ToString();
            int environmentId = GetCurrentEnvironmentId(headers).GetValueOrDefault();
            List<Acesso> cache = WebCacheHelper.GetWebCache<List<Acesso>>(currentUser);

            if (cache.IsNull())
            {
                if (showMessages)
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._CacheInfoNotFound.Code, ErrorConstants._CacheInfoNotFound.Message));
                else
                    return null;
            }

            List<Acesso> TokenList = cache as List<Acesso>;
            Acesso acesso = TokenList.Where(i => i.IdTcsAmbiente == environmentId).FirstOrDefault();

            return acesso;
        }

        public static Guid? GetCurrentCompanyId()
        {
            return GetCurrentCompanyId(null);
        }

        public static Guid? GetCurrentCompanyId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.CurrentCompany;
            }
            else
            {
                string currentCompany = ServiceHelper.GetMessageProperty("CurrentCompany", headers);
                if (currentCompany.IsNullOrEmpty())
                    return null;
                else
                    return Guid.Parse(currentCompany);
            }
        }

        public static string GetCurrentCompanyName()
        {
            return GetCurrentCompanyName(null);
        }

        public static string GetCurrentCompanyName(Dictionary<string, string> headers)
        {
            Guid? companyUid = GetCurrentCompanyId(headers);

            if (companyUid.IsNullOrEmpty())
                return null;

            object cache = GetCompanyInfo(companyUid.ToString());
            string[] companyInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
            return companyInfo[1];
        }

        public static int? GetCurrentCompanyIdLinx(Dictionary<string, string> headers)
        {
            Guid? companyUid = GetCurrentCompanyId(headers);

            if (companyUid.IsNullOrEmpty())
                return null;

            object cache = GetCompanyInfo(companyUid.ToString());
            string[] companyInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
            return int.Parse(companyInfo[0]);
        }
        public static string GetCompanyCnpj(Guid companyUid)
        {
            object cache = GetCompanyInfo(companyUid.ToString());
            string[] companyInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
            return companyInfo[2];
        }

        public static string GetCompanyName(Guid companyUid)
        {
            return GetCurrentCompanyName(new Dictionary<string, string>() { { "CurrentCompany", companyUid.ToString() } });
        }

        public static Guid? GetCurrentEconomicGroupId()
        {
            return GetCurrentEconomicGroupId(null);
        }

        public static Guid? GetCurrentEconomicGroupId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.EconomicGroup;
            }
            else
            {
                string economicGroup = ServiceHelper.GetMessageProperty("EconomicGroup", headers);
                if (economicGroup.IsNullOrEmpty())
                    return null;
                else
                    return Guid.Parse(economicGroup);
            }
        }

        public static string GetCurrentEconomicGroupName()
        {
            return GetCurrentEconomicGroupName(null);
        }

        public static string GetCurrentEconomicGroupName(Dictionary<string, string> headers)
        {
            Guid? companyUid = GetCurrentEconomicGroupId(headers);

            if (companyUid.IsNullOrEmpty())
                return null;

            object cache = GetCompanyInfo(companyUid.ToString());
            string[] companyInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
            return companyInfo[1];
        }

        public static Guid? GetAuthorizationToken()
        {
            return GetAuthorizationToken(null);
        }

        public static Guid? GetAuthorizationToken(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.AuthorizationToken;
            }
            else
            {
                string token = ServiceHelper.GetMessageProperty("AuthorizationToken", headers);
                if (token.IsNullOrEmpty())
                    return null;
                else
                    return Guid.Parse(token);
            }
        }

        public static string GetTransactionInfo()
        {
            return GetTransactionInfo(null);
        }

        public static string GetTransactionInfo(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return null;
            }
            else
            {
                return ServiceHelper.GetMessageProperty("TransactionInfo", headers);
            }
        }

        public static string GetCurrentUserName()
        {
            return GetCurrentUserName(null);
        }

        public static string GetCurrentUserName(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.CurrentUserName;
            }
            else
            {
                Guid? userUid = GetCurrentUserUid(headers);
                UserInfo userInfo = GetUserInfo(headers);
                return userInfo.IsNull() ? null :  userInfo.NomeUsuario;
            }
        }

        public static string GetCurrentUserAuthenticationName()
        {
            return GetCurrentUserAuthenticationName(null);
        }

        public static string GetCurrentUserAuthenticationName(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.CurrentUserName;
            }
            else
            {
                Guid? userUid = GetCurrentUserUid(headers);
                return GetUserInfo(headers).NomeAutenticacao;
            }
        }

        public static int? GetCurrentApplicativeId()
        {
            return GetCurrentApplicativeId(null);
        }

        public static int? GetCurrentApplicativeId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.ApplicativeId;
            }
            else
            {
                int environmentId = GetCurrentEnvironmentId(headers).GetValueOrDefault();

                if (environmentId.IsNullOrEmpty())
                    return null;

                AutorizacaoDomainService ds = new AutorizacaoDomainService();
                object cache = ds.UpdateEnvironmentInfo(environmentId);
                string[] tcsAmbienteInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                return Convert.ToInt32(tcsAmbienteInfo[3]);
            }
        }

        public static string GetcurrentAplicativeName()
        {
            return GetCurrentAplicativeName(null);
        }

        public static string GetCurrentAplicativeName(Dictionary<string, string> headers)
        {
            int environmentId = GetCurrentEnvironmentId(headers).GetValueOrDefault();

            if (environmentId.IsNullOrEmpty())
                return null;

            AutorizacaoDomainService ds = new AutorizacaoDomainService();
            object cache = ds.UpdateEnvironmentInfo(environmentId);
            string[] tcsAmbienteInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
            return tcsAmbienteInfo[4];
        }

        public static bool HasAppTokenControl()
        {
            //Alterada a chave para verificar por Ambiente
            //Caso não encontre o parâmetro retorna true para indicar que possui controle por token

            //Guid? application = BusinessUserServiceHelper.GetCurrentApplicationId();
            int? environment = BusinessUserServiceHelper.GetCurrentEnvironmentId();
            if (!environment.IsNullOrEmpty())
            {
                //string key = application.Value.ToString() + "_HasTokenControl";
                string key = environment.ToString() + "_HasTokenControl";
                var cache = WebCacheHelper.GetWebCache(key);

                if (cache.IsNull())
                {
                    AutorizacaoDomainService ctx = new AutorizacaoDomainService();
                    //Dictionary<string, string> appVar = new Dictionary<string, string>();
                    //appVar.Add("TCS_APLICACAO", application.Value.ToString());
                    //Variação desabilitada - verificar futuramente
                    string parameterValue = LinxBusinessParameters.GetParameter<string>("POSSUI_CONTROLE_POR_TOKEN", null);
                    bool hasControl = (parameterValue.IsNullOrEmpty() ? true : Convert.ToBoolean(parameterValue));
                    WebCacheHelper.UpdateWebCache(key, hasControl, 24);
                    return hasControl;
                }
                else
                    return Convert.ToBoolean(cache);
            }

            return true;
            //return false;
        }

        public static int? GetApplicativeIdByMediaUse(int usabilityId)
        {
            int defaultAppId = 1; //UX is the default
            int? appId = GetCurrentApplicativeId();

            if (!appId.IsNull() && appId != defaultAppId)
            {
                MultimidiaDomainService service = new MultimidiaDomainService();
                var usabilityAppId = (from r in service.GetDocMultimidiaConfig()
                                      where r.LxUsoMultimidia == usabilityId && (r.IdTcsAplicativo == appId || r.IdTcsAplicativo == defaultAppId)
                                      orderby r.IdTcsAplicativo descending
                                      select r.IdTcsAplicativo).FirstOrDefault();

                if (!usabilityAppId.IsNullOrEmpty())
                    appId = usabilityAppId;
            }

            return appId;
        }

        public static Guid? GetCurrentApplicationId()
        {
            return GetCurrentApplicationId(null);
        }

        public static Guid? GetCurrentApplicationId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.ApplicationId;
            }
            else
            {
                string applicationId = ServiceHelper.GetMessageProperty("Application", headers);
                if (applicationId.IsNullOrEmpty())
                    return null;
                else
                    return Guid.Parse(applicationId);
            }
        }

        public static int? GetApplicationId(Guid applicationUid)
        {
            if (applicationUid.IsNullOrEmpty())
            {
                return null;
            }

            int? applicationId = null;
            string cacheKey = String.Format("ApplicationInfo_{0}", applicationUid.ToString());
            string cache = WebCacheHelper.GetWebCache<string>(cacheKey);

            if (cache.IsNull())
            {
                Aplicacao.AplicacaoDomainService ds = new Aplicacao.AplicacaoDomainService();
                var aplicacao = ds.GetTcsAplicacaoNoAssociations().Where(i => i.UidAplicacao == applicationUid).Select(i => i.IdAplicacao).ToList();

                if (!aplicacao.IsNullOrEmpty() && aplicacao.Count() > 0)
                {
                    applicationId = aplicacao[0];
                    WebCacheHelper.UpdateWebCache(cacheKey, string.Format("{0}", applicationId), 720);
                }

            }
            else
                applicationId = int.Parse(cache);

            return applicationId;
        }

        public static Guid? GetCurrentAccessGroupId()
        {
            return GetCurrentAccessGroupId(null);
        }

        public static Guid? GetCurrentAccessGroupId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.AccessGroup;
            }
            else
            {
                string accessGroupId = ServiceHelper.GetMessageProperty("AccessGroup", headers);
                if (accessGroupId.IsNullOrEmpty())
                    return null;
                else
                    return Guid.Parse(accessGroupId);
            }
        }

        public static int? GetCurrentIdLinxEnvironment()
        {
            return GetCurrentIdLinxEnvironment(null);
        }

        public static int? GetCurrentIdLinxEnvironment(Guid currentCompanyUid)
        {
            return GetCurrentIdLinxEnvironment(new Dictionary<string, string>() { { "CurrentCompany", currentCompanyUid.ToString() } });
        }

        public static int? GetCurrentIdLinxEnvironment(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.IdLinx;
            }
            else
            {
                string companyUid = ServiceHelper.GetMessageProperty("CurrentCompany", headers);

                if (companyUid.IsNullOrEmpty())
                    return null;

                object cache = GetCompanyInfo(companyUid);
                string[] companyInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                return Convert.ToInt32(companyInfo[0]);
            }
        }

        private static object GetCompanyInfo(string companyUid)
        {
            AutorizacaoDomainService ds = new AutorizacaoDomainService();
            return ds.UpdateCompanyInfo(Guid.Parse(companyUid));
        }
        public static Guid GetCompanyUid(int idLinx)
        {
            string cacheKey = "Company_" + idLinx;
            string cache = WebCacheHelper.GetWebCache<string>(cacheKey);

            if (cache.IsNullOrEmpty())
            {
                Empresa.EmpresaDomainService dsEmpresa = new Empresa.EmpresaDomainService();
                cache = dsEmpresa.GetTcsEmpresaAutenticacaoNoAssociations().Where(i => i.IdLinx == idLinx).Select(i => i.UidEmpresa).FirstOrDefault().ToString();
                WebCacheHelper.UpdateWebCache(cacheKey, cache, 720);
            }
            return Guid.Parse(cache);
        }

        public static int? GetCurrentIdGpecon()
        {
            return GetCurrentIdGpecon(null);
        }

        public static int? GetCurrentIdGpecon(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.IdGpecon;
            }
            else
            {
                string economicGroup = ServiceHelper.GetMessageProperty("EconomicGroup", headers);

                if (economicGroup.IsNullOrEmpty())
                    return null;

                object cache = GetCompanyInfo(economicGroup);
                string[] companyInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                return Convert.ToInt32(companyInfo[0]);
            }
        }

        public static int? GetCurrentEnvironmentId()
        {
            return GetCurrentEnvironmentId(null);
        }

        public static int? GetCurrentEnvironmentId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.Environment;
            }
            else
            {
                string environmentId = ServiceHelper.GetMessageProperty("Environment", headers);
                if (environmentId.IsNullOrEmpty())
                    return null;
                else
                    return int.Parse(environmentId);
            }
        }

        public static string GetCurrentEnvironmentName()
        {
            return GetCurrentEnvironmentName(null);
        }

        public static string GetCurrentEnvironmentName(Dictionary<string, string> headers)
        {
            int environmentId = GetCurrentEnvironmentId(headers).GetValueOrDefault();

            if (environmentId.IsNullOrEmpty())
                return null;

            return GetEnvironmentName(environmentId);
        }

        public static string GetEnvironmentName(int environmentId)
        {
            AutorizacaoDomainService ds = new AutorizacaoDomainService();
            object cache = ds.UpdateEnvironmentInfo(environmentId);
            string[] tcsAmbienteInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
            return tcsAmbienteInfo[2];
        }

        public static int? GetRelatedEnvironment()
        {
            return GetRelatedEnvironment(null);
        }

        public static int? GetRelatedEnvironment(Dictionary<string, string> headers)
        {
            Acesso acesso = GetAcesso(headers, false);
            return (acesso.IsNull() ? null : acesso.IdAmbienteRelacionado);
        }

        public static Dictionary<string, string> GetEnvironmentInfo(int applicativeId, Dictionary<string, string> headers = null)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();

            if (LocalServiceBus.Enabled)
                return GetHeadersByEnvironment(GetCurrentEnvironmentId().GetValueOrDefault(0), null);

            int idTcsAplicativoHeaders = GetCurrentApplicativeId(headers).GetValueOrDefault(0);

            if (applicativeId == 0)
                applicativeId = GetCurrentApplicativeId().GetValueOrDefault();

            if (applicativeId == idTcsAplicativoHeaders)
            {
                int idAmbiente = GetCurrentEnvironmentId(headers).GetValueOrDefault(0);
                info = GetHeadersByEnvironment(idAmbiente, headers);
            }
            else
            {
                int? idAmbienteRelacionado = GetAcesso(headers).IdAmbienteRelacionado;
                if (idAmbienteRelacionado.IsNull())
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._InvalidRelatedEnvironmentInfo.Code, ErrorConstants._InvalidRelatedEnvironmentInfo.Message));

                AutorizacaoDomainService ds = new AutorizacaoDomainService();
                object cache = ds.UpdateEnvironmentInfo(idAmbienteRelacionado.Value);
                string[] tcsAmbienteInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);

                if (Convert.ToInt32(tcsAmbienteInfo[3]) != applicativeId)
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._InvalidIdAplicativeInfo.Code, ErrorConstants._InvalidIdAplicativeInfo.Message));

                info = GetHeadersByEnvironment(idAmbienteRelacionado.Value, headers);

                if (info.Count() == 0)
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._InvalidRelatedEnvironmentInfo.Code, ErrorConstants._InvalidRelatedEnvironmentInfo.Message));
            }
            return info;
        }

        private static Dictionary<string, string> GetHeadersByEnvironment(int environmentId, Dictionary<string, string> headers)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();

            if (LocalServiceBus.Enabled)
            {
                info.Add("Application", GetCurrentApplicationId().GetValueOrDefault().ToString());
                info.Add("CurrentCompany", GetCurrentCompanyId().GetValueOrDefault().ToString());
                info.Add("Environment", GetCurrentEnvironmentId().GetValueOrDefault().ToString());
                info.Add("CurrentUser", GetCurrentUserUid().GetValueOrDefault().ToString());
                info.Add("EconomicGroup", GetCurrentEconomicGroupId().GetValueOrDefault().ToString());
            }
            else
            {
                AutorizacaoDomainService ds = new AutorizacaoDomainService();
                object cache = ds.UpdateEnvironmentInfo(environmentId);
                string[] tcsAmbienteInfo = cache.ToString().Split(new string[] { "|" }, StringSplitOptions.None);

                info.Add("Application", tcsAmbienteInfo[0]);
                info.Add("CurrentCompany", tcsAmbienteInfo[1]);
                info.Add("Environment", environmentId.ToString());

                Guid uidGrupoEconomico = GetCurrentEconomicGroupId(headers).GetValueOrDefault(Guid.Empty);
                Guid uidUsuario = GetCurrentUserUid(headers).GetValueOrDefault(Guid.Empty);
                List<Acesso> TokenList = WebCacheHelper.GetWebCache<List<Acesso>>(uidUsuario.ToString());

                if (cache.IsNull())
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._CacheInfoNotFound.Code, ErrorConstants._CacheInfoNotFound.Message));

                Acesso acesso = TokenList.Where(i => i.IdTcsAmbiente == environmentId).FirstOrDefault();
                if (acesso.IsNull())
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._CacheInfoNotFound.Code, ErrorConstants._CacheInfoNotFound.Message));

                info.Add("CurrentUser", uidUsuario.ToString());
                info.Add("AuthorizationToken", acesso.Token.ToString());
                info.Add("EconomicGroup", uidGrupoEconomico.ToString());
            }

            return info;
        }

        public static Dictionary<string, string> GetRelatedEnvironmentInfo()
        {
            return GetRelatedEnvironmentInfo(null);
        }

        public static Dictionary<string, string> GetRelatedEnvironmentInfo(Dictionary<string, string> headers)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();

            if (LocalServiceBus.Enabled)
                return info;

            int idAmbienteRelacionado = GetAcesso(headers).IdAmbienteRelacionado.GetValueOrDefault(0);

            if (!idAmbienteRelacionado.IsNullOrEmpty())
            {
                info = GetHeadersByEnvironment(idAmbienteRelacionado, headers);
            }

            return info;
        }

        public static int? GetCurrentIdLinx(string connectionName, int applicativeId, Dictionary<string, string> headers = null)
        {
            Dictionary<string, string> appHeaders = GetEnvironmentInfo(applicativeId, headers);
            return GetCurrentIdLinx(connectionName, appHeaders);
        }

        public static int? GetCurrentIdLinx(string connectionName)
        {
            return GetCurrentIdLinx(connectionName, null);
        }

        public static int? GetCurrentIdLinx(string connectionName, Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
            {
                return LocalServiceBus.IdLinx;
            }
            else
            {
                string result = GetIdLinxInfo(connectionName, headers);

                if (result.IsNullOrEmpty())
                    return null;

                if (result.Left(1) == "[")
                    throw new DomainException(String.Format("{0} - {1}", ErrorConstants._IdLinxNotFound.Code, ErrorConstants._IdLinxNotFound.Message));

                return Convert.ToInt32(result.Left("[##]"));
            }
        }

        internal static string GetIdLinxInfo(string connectionName, Dictionary<string, string> headers)
        {
            bool isControleSistema = connectionName.ToUpper() == "CONTROLESISTEMA";
            int? environment = GetCurrentEnvironmentId(headers);
            int? idLinxEnvironment = GetCurrentIdLinxEnvironment(headers);
            int? idTcsAplicativo = GetCurrentApplicativeId(headers);
            int? relatedEnvironment = isControleSistema ? null : GetRelatedEnvironment(headers);

            string cacheKey = string.Format("{0}-{1}--{2}", connectionName, environment, relatedEnvironment.ToString() ?? "");
            string cache = WebCacheHelper.GetWebCache<string>(cacheKey);
            int idLinx = idLinxEnvironment.GetValueOrDefault();
            string stringConexao = string.Empty;

            if (cache.IsNull())
            {
                Ambiente.AmbienteDomainService context = new Ambiente.AmbienteDomainService();

                var query = (from result in context.GetTcsAmbienteConexaoNoAssociations().Where(i => i.NomeConexao == connectionName && i.IdTcsAmbiente == environment)
                             select new { Servidor = result.NomeServidor, BancoDados = result.NomeBanco, Conexao = result.NomeConexao, StringConexao = result.StringConexao }).FirstOrDefault();

                if (!query.IsNull())
                {
                    stringConexao = GetConnectionString(query.Servidor, query.BancoDados, query.Conexao, query.StringConexao);
                }
                //Procura no ambiente relacionado.
                else if (!isControleSistema && !relatedEnvironment.IsNull())
                {
                    var query1 = (from result in context.GetTcsAmbienteConexaoNoAssociations().Where(i => i.NomeConexao == connectionName && i.IdTcsAmbiente == relatedEnvironment)
                                  select new { Servidor = result.NomeServidor, BancoDados = result.NomeBanco, Conexao = result.NomeConexao, StringConexao = result.StringConexao, IdLinx = result.IdLinx }).FirstOrDefault();

                    if (!query1.IsNull())
                    {
                        idLinx = query1.IdLinx;
                        stringConexao = GetConnectionString(query1.Servidor, query1.BancoDados, query1.Conexao, query1.StringConexao);
                    }
                }

                //Verifica no Web.Config
                if (stringConexao.IsNullOrEmpty())
                {
                    var connection = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];

                    if (connection.IsNullOrEmpty())
                    {

                        string exceptionMessage = String.Format("{0} - {1} - \"{2}\"", ErrorConstants._ConnectionStringNotFound.Code, ErrorConstants._ConnectionStringNotFound.Message, connectionName);

                        Dictionary<string, string> variation = new Dictionary<string, string>();
                        variation.Add("TCS_USUARIO", BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault().ToString());

                        try
                        {
                            if (!isControleSistema && LinxBusinessParameters.GetParameter<bool>("DETALHA_ERROS_AUTORIZACAO", variation))
                            {
                                var currentCompany = GetCurrentCompanyId(headers);
                                var currentUser = GetCurrentUserUid(headers);
                                var economicGroup = GetCurrentEconomicGroupId(headers);
                                var application = GetCurrentApplicationId(headers);

                                exceptionMessage = String.Format("{0}\n\nInformações :", exceptionMessage);
                                //Id Ambiente
                                exceptionMessage = string.Format("{0}\n\nId Ambiente : {1}", exceptionMessage, environment);
                                //Id Ambiente Relacionado
                                exceptionMessage = string.Format("{0}\nId Ambiente Relacionado : {1}", exceptionMessage, relatedEnvironment);
                                //Headers
                                exceptionMessage = string.Format("{0}\n\nHeaders :", exceptionMessage);
                                //CurrentCompany
                                exceptionMessage = string.Format("{0}\n\nCurrentCompany : {1}", exceptionMessage, currentCompany);
                                //CurrentUser
                                exceptionMessage = string.Format("{0}\nCurrentUser : {1}", exceptionMessage, currentUser);
                                //Economic Group
                                exceptionMessage = string.Format("{0}\nEconomicGroup : {1}", exceptionMessage, economicGroup);
                                //Environment
                                exceptionMessage = string.Format("{0}\nEnvironment : {1}", exceptionMessage, environment);
                                //Application
                                exceptionMessage = string.Format("{0}\nApplication : {1}", exceptionMessage, application);
                            }
                        }
                        catch { }

                        throw new Exception(exceptionMessage);
                    }

                    stringConexao = connection.ConnectionString;

                }

                cache = string.Format("{0}[##]{1}", idLinx, stringConexao);
                WebCacheHelper.UpdateWebCache(cacheKey, cache, 720);
            }
            return cache.ToString();
        }
        private static string GetConnectionString(string server, string database, string provider, string connectionString)
        {
            //@banco = Banco de Dados / @provider = Nome Provider - BM / @servidor = Servidor 
            return connectionString.Replace("@BANCO", database).Replace("@banco", database).Replace("@SERVIDOR", server).Replace("@servidor", server).Replace("@PROVIDER", provider).Replace("@provider", provider);
        }

        public static string GetCustomSearchById(Int64 idSearch)
        {
            if (LocalServiceBus.Enabled)
            {
                return null;
            }
            else
            {
                FiltroDomainService ds = new FiltroDomainService();

                var search = (from result in ds.GetTcsFiltroNoAssociations().Where(i => i.IdFiltro == idSearch)
                              select result.ComandoFiltro).FirstOrDefault();

                return search;
            }
        }

        public static Dictionary<Int64, string> GetCustomSearchList(string entityName)
        {
            if (LocalServiceBus.Enabled)
                return null;
            else
            {
                FiltroDomainService ds = new FiltroDomainService();

                var query = (from result in ds.GetTcsFiltroNoAssociations().Where(i => i.NomeEntidadeBm == entityName && i.LxTipoFiltro == 2 && i.Parametros == null)
                             select new { result.IdFiltro, result.DescFiltro }).ToDictionary(i => i.IdFiltro, i => i.DescFiltro);

                return query;
            }
        }

        public static bool AddMessage(string titulo, string corpo, List<EntitySearch> filtro, DateTime? dataEnvio, int idLinx, byte lxTipoMensagem)
        {
            Mensagem.MensagemDomainService ds = new Mensagem.MensagemDomainService();
            return ds.AddTcsMensagem(titulo, corpo, SerializationManager<List<EntitySearch>>.ObjectToString(filtro), dataEnvio, idLinx, lxTipoMensagem);
        }

        public static bool GetAuditIsEnabled(int? idLinx)
        {
            bool retValue = false;

            var parameterValue = GetParameterValueCached("AUDITORIA_HABILITADA", idLinx, false);
            if (!parameterValue.IsNullOrEmpty())
                bool.TryParse(parameterValue, out retValue);
            return retValue;
        }

        public static string[] GetAuditIgnoredTables(int? idLinx)
        {
            return ConvertStringInArray(GetParameterValueCached("AUDITORIA_TABELAS_IGNORADAS", idLinx, string.Empty));
        }

        public static string[] GetAuditIgnoredSchemas(int? idLinx)
        {
            return ConvertStringInArray(GetParameterValueCached("AUDITORIA_SCHEMAS_IGNORADAS", idLinx, string.Empty));
        }

        #region Parameter and Cache Helper
        private static string GetParameterValueCached(string parameterName, int? idLinx, object defaultValue)
        {
            var _idLinx = GetIdLinx(idLinx);
            string cacheKey = string.Format("{0}_ID_LINX_{1}", parameterName, _idLinx);

            string value = WebCacheHelper.GetWebCache<string>(cacheKey);
            if (value == null)
            {
                value = LinxBusinessParameters.GetParameter<string>(parameterName, null);
                WebCacheHelper.UpdateWebCache(cacheKey, value ?? defaultValue);
            }

            return value;
        }

        private static int GetIdLinx(int? idLinx)
        {
            if (idLinx.HasValue)
                return idLinx.Value;
            else return GetCurrentIdLinx("ControleSistema") ?? 0;
        }

        private static string[] ConvertStringInArray(string value)
        {
            string[] tables = new string[] { };
            char[] charSeparator = new char[] { ',', ';', '|' };

            if (!value.IsNullOrEmpty())
            {
                tables = value.Split(charSeparator, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToArray();
            }

            return tables;
        }
        #endregion

        public static Guid AddEntySearchToCache(string serializedEntitySearch, string jEntitySearch)
        {
            Guid entitySearchUid = Guid.NewGuid();
            string[] cacheValue = new string[] { serializedEntitySearch, jEntitySearch };
            WebCacheHelper.AddWebCache(entitySearchUid.ToString(), cacheValue, 1);
            return entitySearchUid;
        }

        public static string[] GetEntitySearchFromCache(Guid entitySearchUid)
        {
            return WebCacheHelper.GetWebCache<string[]>(entitySearchUid.ToString());
        }
    }
}
