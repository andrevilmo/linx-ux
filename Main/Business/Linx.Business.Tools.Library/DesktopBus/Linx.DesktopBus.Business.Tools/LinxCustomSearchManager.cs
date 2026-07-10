using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.Collections.ObjectModel;

namespace Linx.Business.Tools
{
    public class LinxCustomSearchManager
    {
        LinxCustomSearchUtils searchUtils = new LinxCustomSearchUtils();

        public LinxCustomSearchUtils CustomSearchUtils
        {
            get { return searchUtils; }
        }

        public void LoadCustomSearchManager(Guid uidObject, object dataAnalysis)
        {
            searchUtils.LoadEntities(dataAnalysis, uidObject);
        }

        public string ValidateCustomSearch(List<FilterItem> lstFilterItem)
        {
            List<ParameterInfo> lstParametersSearch = new List<ParameterInfo>();
            List<EntitySearch> originalSearch = null;
            List<EntitySearch> evaluatedSearch = new List<EntitySearch>();
            List<EntitySearch> entities = new List<EntitySearch>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string comandoFiltro;

            int counter = -1;

            foreach (FilterItem item in lstFilterItem)
            {
                comandoFiltro = item.XmlFilter;
                originalSearch = SerializationManager<List<EntitySearch>>.StringToObject(comandoFiltro);

                //aqui
                ////Pré definidos
                //if (comandoFiltro.Contains("PredefinedFilter"))
                //    searchUtils.EvaluatePredefinedFilters(originalSearch, evaluatedSearch);
                //else
                    evaluatedSearch = SerializationManager<List<EntitySearch>>.StringToObject(comandoFiltro);

                //Parâmetro usuário
                if (comandoFiltro.Contains("@"))
                {
                    //var userParameter = item.ParameterList.Where(i => i.Key.Contains("@"));
                    //if (userParameter.Count() ==0)
                    if (item.ParameterValue.IsNullOrEmpty())
                        throw new Exception(String.Format(@"Favor informar o valor do parâmetro para a pesquisa : ""{0}"".".Translate(), item.Description));

                    searchUtils.EvaluateUserParameter(item.ParameterValue, evaluatedSearch, parameters);
                }

                //Parâmetros sistema
                if (comandoFiltro.Contains("#"))
                {
                    foreach (EntitySearch entitySearch in evaluatedSearch)
                    {
                        List<EntitySearchExpression> expressions = entitySearch.Expressions.Where(i => i.Value.ToString().Contains("#")).ToList();

                        foreach (EntitySearchExpression expression in expressions)
                        {
                            string parameterName = expression.Value.ToString().Replace("#", "");
                            var parameter = item.ParameterList.Where(i => i.Key == parameterName);

                            if (parameter.Count() == 0)
                                throw new Exception(string.Format("Valor do parâmetro {0} não informado".Translate(), parameterName));

                            int fieldPosition = entitySearch.Expressions.IndexOf(expression) - 2;
                            expression.Value = searchUtils.ConvertFieldValue(parameter.First().Value, searchUtils.GetFieldtype(entitySearch.EntityName, entitySearch.Expressions[fieldPosition].Value.ToString()));
                        }
                    }
                }

                if (entities.Count > 0)
                {
                    EntitySearch condition;
                    if (item.OperatorAnd)
                    {
                        condition = new EntitySearch("AndEntityCondition");
                        condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "&&"));
                    }
                    else
                    {
                        condition = new EntitySearch("OrEntityCondition");
                        condition.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "||"));
                    }

                    entities.AddRange(new List<EntitySearch>() { condition });
                    counter--;
                }

                entities.AddRange(evaluatedSearch);
            }
            return SerializationManager<List<EntitySearch>>.ObjectToString(entities);
        }
    }
}
