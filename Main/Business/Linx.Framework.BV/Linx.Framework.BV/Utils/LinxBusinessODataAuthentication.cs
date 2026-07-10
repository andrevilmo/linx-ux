using Linx.Framework.BV.Autorizacao;
using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linx.Framework.BV
{
    public static class LinxBusinessODataAuthentication
    {
        public static Dictionary<int, string> ODataAuthentication(string authenticationParameters)
        {
            Dictionary<int, string> indexedHeaders = null;

            var tokens = Encoding.Default.GetString(Convert.FromBase64String(authenticationParameters)).Split(':');

            if (tokens.Length < 2)
                throw new Exception(String.Format("{0} - {1}", ErrorConstants._UserBadNameOrPassword.Code, ErrorConstants._UserBadNameOrPassword.Message));

            indexedHeaders = WebCacheHelper.GetWebCache<Dictionary<int, string>>(authenticationParameters);

            if (VerifyAuthenticationProblems(ref indexedHeaders))
                WebCacheHelper.RemoveWebCache(authenticationParameters);

            if (indexedHeaders == null)
            {
                UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
                Autorizacao.AutorizacaoDomainService dsAutorizacao = new AutorizacaoDomainService();

                string nomeAutenticacao = tokens[0];

                var ambiente = (from result in ds.GetTcsUsuarioAcessoAmbienteNoAssociations()
                                where result.NomeAutenticacao == nomeAutenticacao //&& result.IdAplicativo == 1 aqui
                                orderby result.IndicaAcessoPadrao descending
                                select new { IdAmbiente = result.IdTcsAmbiente, UidAplicacao = result.UidAplicacao }).FirstOrDefault();

                if (ambiente != null)
                {
                    indexedHeaders = dsAutorizacao.AuthenticateOData(tokens[0], tokens[1], ambiente.IdAmbiente);
                    indexedHeaders.Add(10, nomeAutenticacao);
                    WebCacheHelper.UpdateWebCache(authenticationParameters, indexedHeaders);
                }
            }
            return indexedHeaders;
        }

        private static bool VerifyAuthenticationProblems(ref Dictionary<int, string> indexedHeaders)
        {
            bool hasProblem = false;
            if (indexedHeaders != null)
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();

                headers.Add("CurrentCompany", indexedHeaders[1]);
                headers.Add("AuthorizationToken", indexedHeaders[2]);
                headers.Add("CurrentUser", indexedHeaders[3]);
                headers.Add("AccessGroup", indexedHeaders[4]);
                headers.Add("EconomicGroup", indexedHeaders[5]);
                headers.Add("Environment", indexedHeaders[6]);
                headers.Add("Application", indexedHeaders[8]);

                try
                {
                    Autorizacao.AutorizacaoDomainService authorization = new Autorizacao.AutorizacaoDomainService();
                    authorization.ValidateToken(Guid.Parse(indexedHeaders[3]), BusinessUserServiceHelper.GetAuthorizationToken(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentApplicationId(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentCompanyId(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentAccessGroupId(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentEnvironmentId(headers).GetValueOrDefault());
                }
                catch
                {
                    indexedHeaders = null;
                    hasProblem = true;
                }
            }
            return hasProblem;
        }
    }
}