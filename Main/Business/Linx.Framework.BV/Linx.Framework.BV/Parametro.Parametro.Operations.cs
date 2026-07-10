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
using Linx.Framework.ControleSistema.BM;
using Linx.Framework.BV.ParametroAutorizacao;

namespace Linx.Framework.BV.Parametro
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class ParametroDomainService
    {
        [Query(HasSideEffects = true)]
        public List<ParametroValor> GetParameterValue(string serializedParameterList, Dictionary<string, string> headers = null)
        {
            List<ParameterRequestInfo> parameterList = (serializedParameterList.IsNullOrEmpty() ? new List<ParameterRequestInfo>() : SerializationManager<List<ParameterRequestInfo>>.StringToObject(serializedParameterList));
            List<ParametroValor> listParametroValor = new List<ParametroValor>();

            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode(headers) == "POSUX")
            {
                List<EntitySearch> search = new List<EntitySearch>();
                EntitySearch condition = new EntitySearch("LjvParametro");

                foreach (ParameterRequestInfo parameterName in parameterList)
                {
                    if (condition.Expressions.Count > 0)
                        condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "||"));

                    condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "TituloParametro"));
                    condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                    condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, parameterName.Title));
                }
                search.Add(condition);
                LojaParametro.LojaParametroDomainService dsLoja = new LojaParametro.LojaParametroDomainService(headers);
                var parametros = (from result in dsLoja.GetLjvParametroByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(search))
                                  select new
                                  {
                                      TituloParametro = result.TituloParametro,
                                      ValorParametro = result.ValorParametro,
                                      LxDatatypeParametro = result.LxDatatypeParametro
                                  }).ToList();

                foreach (var parametro in parametros)
                {
                    Type parameterType = GetParameterType(parametro.LxDatatypeParametro);
                    listParametroValor.Add(new ParametroValor() { TituloParametro = parametro.TituloParametro, ValorParametro = parametro.ValorParametro, TipoValorParametro = parameterType });
                }
            }
            else
            {
                ParametroAutorizacaoDomainService ds = new ParametroAutorizacaoDomainService();
                int idTcsAplicativo = BusinessUserServiceHelper.GetCurrentApplicativeId(headers).GetValueOrDefault();

                foreach (ParameterRequestInfo parameterName in parameterList)
                {
                    var parametro = (from result in ds.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.TituloParametro == parameterName.Title && (i.IdTcsAplicativo == idTcsAplicativo || i.IdTcsAplicativo == 1))
                                     select new
                                     {
                                         IdParametro = result.IdParametro,
                                         TituloParametro = result.TituloParametro,
                                         PermiteVariacaoPorEntidade = result.PermiteVariacaoPorEntidade,
                                         LxDatatypeParametro = result.LxDatatypeParametro,
                                         IdTcsAplicativo = result.IdTcsAplicativo
                                     }).OrderByDescending(i => i.IdTcsAplicativo).FirstOrDefault();


                    if (!parametro.IsNull())
                    {
                        ParametroDomainService dsParametro = new ParametroDomainService(headers);
                        var tcsParametroValorList = (from result in dsParametro.GetTcsParametroValorP1NoAssociations().Where(i => i.IdParametro == parametro.IdParametro)
                                                     select new { ValorParametro = result.ValorParametro }).ToList();

                        string parameterValue = string.Empty;

                        //Valor padrão
                        if (tcsParametroValorList.Count() > 0)
                            parameterValue = tcsParametroValorList.First().ValorParametro;

                        //Se permite variação e possui tabelas cadastradas.
                        if (parametro.PermiteVariacaoPorEntidade)
                        {
                            var tcsParametroTabelaSelecaoList = (from result in ds.GetTcsParametroTabelaSelecaoAutorizacaoNoAssociations().Where(i => i.IdParametro == parametro.IdParametro)
                                                                 select new { UidTabela = result.UidTabela, NomeTabela = result.NomeTabela, LxParametroHierarquia = result.LxParametroHierarquia }).ToList();

                            int variationCount = tcsParametroTabelaSelecaoList.Count();
                            bool variationFound = false;
                            ParameterRequestInfo parameterRequestInfo = parameterList.Where(i => i.Title.ToUpper().Trim() == parametro.TituloParametro.ToUpper()).FirstOrDefault();

                            //Verifica a quantidade de parâmetros enviados
                            if (parameterRequestInfo.VariationValues.IsNull() || parameterRequestInfo.VariationValues.Count() < variationCount)
                                throw new Exception(String.Format("Favor informar todas as variações para o parâmetro : '{0}'.", parametro.TituloParametro));

                            //se alguma das variações é vazia ou nula
                            if (parameterRequestInfo.VariationValues.Where(i => i.Value.IsNullOrEmpty()).Count() > 0)
                                throw new Exception(String.Format("Um ou mais valores informados para as variações do parâmetro '{0}' está vazio ou nulo.", parametro.TituloParametro));

                            var mandatory = tcsParametroTabelaSelecaoList.Where(i => i.LxParametroHierarquia == 100).FirstOrDefault();
                            var levels = tcsParametroTabelaSelecaoList.Where(i => i.LxParametroHierarquia != 100).OrderByDescending(i => i.LxParametroHierarquia).ToList();

                            var tcsParametroValorVariacaoList = (from result in dsParametro.GetTcsParametroValorVariacaoPNoAssociations().Where(i => i.IdParametro == parametro.IdParametro)
                                                                 select new { ChaveSelecao = result.ChaveSelecao, UidTabela = result.UidTabela, ValorParametro = result.ValorParametro }).ToList();

                            foreach (var level in levels)
                            {
                                var variationValue = parameterRequestInfo.VariationValues.Where(i => i.Key.ToUpper() == level.NomeTabela.ToUpper()).FirstOrDefault();

                                if (variationValue.IsNull())
                                    throw new Exception(string.Format("Não foi informado o valor para a variação '{0}' do parâmetro {1}.", level.NomeTabela, parametro.TituloParametro));

                                var paramVariationValue = tcsParametroValorVariacaoList.Where(i => i.UidTabela == level.UidTabela && i.ChaveSelecao.ToUpper() == variationValue.Value.ToUpper()).FirstOrDefault();

                                if (!paramVariationValue.IsNullOrEmpty())
                                {
                                    parameterValue = paramVariationValue.ValorParametro;
                                    variationFound = true;
                                    break;
                                }
                            }

                            if (!variationFound)
                            {
                                if (!mandatory.IsNull())
                                {
                                    var variationValue = parameterRequestInfo.VariationValues.Where(i => i.Key.ToUpper() == mandatory.NomeTabela.ToUpper()).FirstOrDefault();

                                    if (variationValue.IsNull())
                                        throw new Exception(string.Format("Não foi informado o valor para a variação '{0}' do parâmetro {1}.", mandatory.NomeTabela, parametro.TituloParametro));

                                    var paramVariationValue = tcsParametroValorVariacaoList.Where(i => i.UidTabela == mandatory.UidTabela && i.ChaveSelecao == variationValue.Value).FirstOrDefault();

                                    if (paramVariationValue.IsNullOrEmpty())
                                        throw new Exception(string.Format("Não foi encontrado valor para o parâmetro '{0}'.", parametro.TituloParametro));

                                    parameterValue = paramVariationValue.ValorParametro;
                                }
                            }
                        }

                        Type parameterType = GetParameterType(parametro.LxDatatypeParametro);
                        listParametroValor.Add(new ParametroValor() { TituloParametro = parametro.TituloParametro, ValorParametro = parameterValue, TipoValorParametro = parameterType });

                    }
                }
            }
            return listParametroValor;
        }

        [Ignore]
        public List<ParameterRequestValue> GetParameterValue(List<ParameterRequestInfo> parameterList, Dictionary<string, string> headers = null)
        {
            var values = GetParameterValue(parameterList.IsNullOrEmpty() ? null : SerializationManager<List<ParameterRequestInfo>>.ObjectToString(parameterList), headers);
            return values.Select(e => new ParameterRequestValue() { Title = e.TituloParametro, Value = e.ValorParametro, DataType = e.TipoValorParametro }).ToList();
        }

        private Type GetParameterType(int datatypeParametro)
        {
            Type parameterType;

            //Datatype
            switch (datatypeParametro)
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
            return parameterType;
        }

    }
}
