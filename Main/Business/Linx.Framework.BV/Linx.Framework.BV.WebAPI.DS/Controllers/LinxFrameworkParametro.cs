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
using Linx.Framework.BV.Parametro;
using System.ServiceModel.DomainServices.Server;
using Linx.Framework.BV.ParametroAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkParametroController
    {
        [LinxFrameworkAutorizacaoControllerAuthorize]
        [Route("GetParameterValue"), System.Web.Http.HttpGet()]
        public string GetParameterValue(string serializedParameterList)
        {
            List<ParameterRequestInfo> parameterList = GetRequestInfo(serializedParameterList);
            var parameters = this.repository.Context.GetParameterValue(SerializationManager<List<ParameterRequestInfo>>.ObjectToString(parameterList));
            var q = parameters.Select(p => string.Format("{0}|{1}", p.TituloParametro, p.ValorParametro));
            return string.Join("#", q.ToArray());
        }

        private List<ParameterRequestInfo> GetRequestInfo(string serializedParameterList)
        {
            List<ParameterRequestInfo> parameterList = new List<ParameterRequestInfo>();
            ParameterRequestInfo parameter = null;

            var splitItens = serializedParameterList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var _splitItem in splitItens)
            {
                if (_splitItem.Contains("{"))
                {
                    var indexOf = _splitItem.IndexOf("{");

                    parameter = new ParameterRequestInfo() { Title = _splitItem.Substring(0, indexOf) };
                    var _vartiations = _splitItem.Substring(indexOf + 1, _splitItem.LastIndexOf("}") - indexOf - 1);
                    var vItems = _vartiations.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    if (vItems.Length > 0)
                    {
                        parameter.VariationValues = new Dictionary<string, string>();
                        for (int i = 0; i < vItems.Length; i += 2)
                            if ((i + 1) < vItems.Length)
                                parameter.VariationValues.Add(vItems[i], vItems[i + 1]);
                    }
                }
                else
                    parameter = new ParameterRequestInfo() { Title = _splitItem };
                parameterList.Add(parameter);
            }

            return parameterList;
        }

        [Route("SetParameterValue"), System.Web.Http.HttpPost()]
        public bool SetParameterValue(ParametroValorVariacao parametroValorVariacao)
        {
            ParametroAutorizacaoDomainService ds = new ParametroAutorizacaoDomainService();
            var context = this.repository.Context;

            var parametro = (from result in ds.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.TituloParametro == parametroValorVariacao.TituloParametro)
                             select new { IdParametro = result.IdParametro }).FirstOrDefault();

            if (parametro.IsNull())
                throw new Exception(String.Format("Parâmetro {0} não econtrado !", parametroValorVariacao.TituloParametro));

            var tabelaVariacao = (from result in ds.GetTcsParametroTabelaSelecaoAutorizacaoNoAssociations().Where(i => i.IdParametro == parametro.IdParametro && i.NomeTabela == parametroValorVariacao.NomeTabela)
                                                 select new { UidTabela = result.UidTabela }).FirstOrDefault();

            if (tabelaVariacao.IsNull())
                throw new Exception(string.Format("Parâmetro {0} não possui a variação informada ! '{1}'",parametroValorVariacao.TituloParametro, parametroValorVariacao.NomeTabela));

            TcsParametroValorVariacaoP valorVariacao = (from result in context.GetTcsParametroValorVariacaoPNoAssociations().Where(i => i.IdParametro == parametro.IdParametro && i.UidTabela == tabelaVariacao.UidTabela && i.ChaveSelecao == parametroValorVariacao.ChaveVariacao)
                                                        select result).FirstOrDefault();

            if (valorVariacao.IsNull())
            {
                valorVariacao = new TcsParametroValorVariacaoP() { IdParametro = parametro.IdParametro, UidTabela = tabelaVariacao.UidTabela, ChaveSelecao = parametroValorVariacao.ChaveVariacao, PossuiVariacao = true, ValorParametro = parametroValorVariacao.ValorVariacao };
                context.AddCustomChanges(valorVariacao, null, ChangeOperation.Insert);
            }
            else
            {
                TcsParametroValorVariacaoP valorVariacaoOld = new TcsParametroValorVariacaoP();
                valorVariacaoOld.CopyInstanceFrom(valorVariacao);
                valorVariacao.ValorParametro = parametroValorVariacao.ValorVariacao;
                context.AddCustomChanges(valorVariacao, valorVariacaoOld, ChangeOperation.Update);
            }
            context.SaveCustomChanges();

            return true;
        }

        [Route("GetParameterValueMultiEnvironment"), System.Web.Http.HttpPost()]
        public List<ParametroInfo> GetParameterValueMultiEnvironment(Modulo.EnvironmentInfo[] environments)
        {
            List<ParametroInfo> parametroInfoFull = new List<ParametroInfo>();
            string serializedParameterList = environments.Select(i => i.ParameterList).FirstOrDefault();
            Guid? currentUser = BusinessUserServiceHelper.GetCurrentUserUid();
            Guid? economicGroup = BusinessUserServiceHelper.GetCurrentEconomicGroupId();
            List<ParameterRequestInfo> parameterList = GetRequestInfo(serializedParameterList);

            foreach (Modulo.EnvironmentInfo item in environments)
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("CurrentUser", currentUser.ToString());
                headers.Add("EconomicGroup", economicGroup.ToString());
                headers.Add("Environment", item.EnvironmentId.ToString());
                headers.Add("CurrentCompany", item.CompanyUid.ToString());
                headers.Add("Application", item.ApplicationUid.ToString());
                headers.Add("LoginMode", BusinessUserServiceHelper.GetCurrentLoginMode());

                Parametro.ParametroDomainService dsParametro = new ParametroDomainService(headers);
                List<ParametroValor> parametroValorList = dsParametro.GetParameterValue(SerializationManager<List<ParameterRequestInfo>>.ObjectToString(parameterList), headers);

                foreach (ParametroValor parametroValor in parametroValorList)
                {
                    parametroInfoFull.Add(new ParametroInfo() { IdTcsAmbiente = item.EnvironmentId, TituloParametro = parametroValor.TituloParametro, ValorParametro = parametroValor.ValorParametro });
                }
            }
            return parametroInfoFull;
        }
    }
}
