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
using Linx.Framework.ControleSistema.BM;


namespace Linx.TCS0101.BO.TcsParametro
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsParametroDomainService
    {
        [Query(HasSideEffects = true)]
        public List<ParametroValor> GetParameterValue(string serializedParameterList)
        {
            List<ParameterRequestInfo> parameterList = (serializedParameterList.IsNullOrEmpty() ? new List<ParameterRequestInfo>() : SerializationManager<List<ParameterRequestInfo>>.StringToObject(serializedParameterList));
            List<ParametroValor> listParametroValor = new List<ParametroValor>();
            List<EntitySearch> search = new List<EntitySearch>();
            EntitySearch condition = new EntitySearch("TcsParametro");

            foreach (ParameterRequestInfo parameterName in parameterList)
            {
                if (condition.Expressions.Count > 0)
                    condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "||"));

                condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "TituloParametro"));
                condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, parameterName.Title));
            }
            search.Add(condition);

            List<TcsParametro> tcsParametroList = (from r in this.GetTcsParametroByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(search))
                                                   select r).ToList();

            foreach (TcsParametro parametro in tcsParametroList)
            {
                parametro.FillDetails(this, null);

                string parameterValue = string.Empty;
                Type parameterType;
                ParameterRequestInfo parameterRequestInfo = parameterList.Where(i => i.Title == parametro.TituloParametro).FirstOrDefault();

                //Valor padrão
                if (parametro.TcsParametroValorList.Count() > 0)
                    parameterValue = parametro.TcsParametroValorList.First().ValorParametro;

                //Variação
                if (!parameterRequestInfo.IsNull() && !parameterRequestInfo.VariationValues.IsNull() && parameterRequestInfo.VariationValues.Count() > 0 && parametro.TcsParametroValorVariacaoList.Count() > 0)
                {
                    List<string> variations = new List<string>();
                    foreach (string item in parameterRequestInfo.VariationValues.Keys)
                    {
                        variations.Add(item.ToUpper());
                    }
                    string valorParametro = this.GetVariationValue(parametro, parameterRequestInfo, variations, parameterRequestInfo.VariationValues.Count());
                    parameterValue = valorParametro.IsNullOrEmpty() ? parameterValue : valorParametro;
                }

                //Datatype
                switch (parametro.LxTipoValorParametro)
                {
                    case 1: // Numérico
                        parameterType = typeof(Decimal);
                        break;
                    case 2: // Caractere
                        parameterType = typeof(String);
                        break;
                    case 3: // Data
                        parameterType = typeof(DateTime);
                        break;
                    case 4: // Lógico
                        parameterType = typeof(Boolean);
                        break;
                    default: // Outros
                        parameterType = typeof(String);
                        break;
                }

                listParametroValor.Add(new ParametroValor() { TituloParametro = parametro.TituloParametro, ValorParametro = parameterValue, TipoValorParametro = parameterType });

            }
            return listParametroValor;

            
        }

        [Ignore]
        public List<ParameterRequestValue> GetParameterValue(List<ParameterRequestInfo> parameterList)
        {
            var values = GetParameterValue(parameterList.IsNullOrEmpty() ? null : SerializationManager<List<ParameterRequestInfo>>.ObjectToString(parameterList));
            return values.Select(e => new ParameterRequestValue() { Title = e.TituloParametro, Value = e.ValorParametro, DataType = e.TipoValorParametro  }).ToList();
        }

        [Ignore]
        private string GetVariationValue(TcsParametro tcsParametro, ParameterRequestInfo parameterRequestInfo, List<string> variations, int vCount)
        {
            string parameterValue = string.Empty;
            List<TcsParametroValorVariacao> paramValor = tcsParametro.TcsParametroValorVariacaoList.Where(i => i.TcsParametroChaveSelecaoList.Count() == vCount && i.TcsParametroChaveSelecaoList.Where(r => variations.Contains(r.NomeTabela.ToUpper())).Count() == vCount).ToList();
            var elements = Linx.Mathematics.CombinatoryAnalysis.CombineElements(parameterRequestInfo.VariationValues.ToList(), vCount);

            bool found = false;
            foreach (TcsParametroValorVariacao item in paramValor)
            {
                foreach (var element in elements)
                {
                    foreach (KeyValuePair<string, string> variacao in element)
                    {
                        found = item.TcsParametroChaveSelecaoList.Where(i => i.NomeTabela.ToUpper() == variacao.Key.ToUpper() && i.ChaveSelecao.ToUpper() == variacao.Value.ToUpper()).Count() > 0;
                        if (!found)
                            break;
                    }
                    if (found)
                    {
                        parameterValue = item.ValorParametro;
                        break;
                    }
                }
            }

            if (!found && --vCount > 0)
                parameterValue = this.GetVariationValue(tcsParametro, parameterRequestInfo, variations, vCount);

            return parameterValue;
        }

        

       
    }
}
