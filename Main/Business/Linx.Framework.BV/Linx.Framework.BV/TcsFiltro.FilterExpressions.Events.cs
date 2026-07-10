using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx.TCS0101.BO.TcsParametro;

namespace Linx.TCS0101.BO.TcsFiltro
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class FilterExpressions
	{
        
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpParams(ref IEnumerable<LookUpParams> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            if (entitySearch != null)
            {
                List<LookUpParams> result = new List<LookUpParams>(); 
                string value = entitySearch.GetExpressionValue(propertyName) as string;
                if (!value.IsNullOrEmpty() && !value.Contains("%"))
                {
                    result.Add(new LookUpParams() { Value = value, Type = "" });
                }
                else
                {
                    var expression = entitySearch.Expressions.Where(e => e.Name == "Field" && (e.Value as string) == propertyName).FirstOrDefault();
                    if (expression != null)
                    {
                        expression.Value = "TituloParametro";
                        result.Add(new LookUpParams() { Value = "@ApplicationId", Type = "Variável de Ambiente" });
                        result.Add(new LookUpParams() { Value = "@ModuleGroupId", Type = "Variável de Ambiente" });
                        result.Add(new LookUpParams() { Value = "@UserId", Type = "Variável de Ambiente" });
                        result.Add(new LookUpParams() { Value = "@UserName", Type = "Variável de Ambiente" });

                        TcsParametroDomainService context = new TcsParametroDomainService();
                        result.AddRange((from r in context.GetTcsParametroByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                         select new LookUpParams() { Value = "@" + r.TituloParametro, Type = "Parâmetro" }));
                    }
                }

                searchDefinition = result;
            }
        }

        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTables(ref IEnumerable<LookUpTables> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            if (entitySearch != null)
            {
                string value = entitySearch.GetExpressionValue(propertyName) as string;
                if (!value.IsNullOrEmpty() && !value.Contains("%"))
                {
                    searchDefinition = new List<LookUpTables>() { new LookUpTables() {  Field = value } };
                }
                else
                {
                    var expression = entitySearch.Expressions.Where(e => e.Name == "Field" && (e.Value as string) == propertyName).FirstOrDefault();
                    if (expression != null)
                    {
                        expression.Value = "NomeTabela";
                        TcsAutorizacao.TcsAutorizacaoDomainService context = new TcsAutorizacao.TcsAutorizacaoDomainService();
                        searchDefinition = (from r in context.GetTcsTabelaAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                            select new LookUpTables() { Field = "[" + r.NomeTabela + "]" });
                    }
                }
            }
        }

        /// Replace the automatic search method.
        public static IEnumerable<FilterExpressions> OnSearchingReplacement(List<EntitySearch> entitySearchList)
        {
            int? idFilter = null;
            if (entitySearchList != null && entitySearchList.Count > 0)
            {
                var filter = entitySearchList.FirstOrDefault();
                idFilter = filter.GetExpressionValue("IdFilter") as int?;
            }

            if (idFilter != null)
            {
                TcsFiltroDomainService context = new TcsFiltroDomainService();
                string command = context.GetTcsFiltro().Where(e => e.IdFiltro == idFilter.Value).Select(e => e.ComandoFiltro).FirstOrDefault();
                if (!command.IsNullOrEmpty())
                {
                    List<FilterExpressions> result = null;
                    try
                    {
                        result = SerializationManager<List<FilterExpressions>>.StringToObject(command);
                    }
                    catch { }

                    if (result == null)
                        return new List<FilterExpressions>();
                    else
                        return result;
                }
                else
                    return new List<FilterExpressions>();
            }
            else
                return new List<FilterExpressions>();
        }

        /// Execute on transaction context starting.
        public static void OnTransactingContextChanges(TcsFiltroDomainService context, ChangeSetEntry[] entities)
        {
            List<FilterExpressions> expressionsList = new List<FilterExpressions>();;
            foreach (var entry in entities.Where(e => e.Operation != DomainOperation.Delete))
            {
                if (entry.Entity is FilterExpressions)
                    expressionsList.Add(entry.Entity as FilterExpressions);            
            }

            if (expressionsList.Count > 0)
            {
                foreach (int idFilter in expressionsList.Select(f => f.IdFilter).Distinct().ToArray())
                {
                    string command = SerializationManager<List<FilterExpressions>>.ObjectToString(expressionsList.Where(e => e.IdFilter == idFilter).OrderBy(e => e.Id).ToList());
                    TcsFiltro oldEntity = new TcsFiltro() { IdFiltro = idFilter, ComandoFiltro = "", DescFiltro = "", LxTipoFiltro = 0, LxTipoObjeto = 0, NomeUsuario = "" };
                    TcsFiltro newEntity = new TcsFiltro() { IdFiltro = idFilter, ComandoFiltro = command , DescFiltro = "", LxTipoFiltro = 0, LxTipoObjeto = 0, NomeUsuario = ""};
                    context.AddCustomChanges(newEntity, oldEntity, ChangeOperation.Update);
                }
            }

        }

        
                
    }
}
