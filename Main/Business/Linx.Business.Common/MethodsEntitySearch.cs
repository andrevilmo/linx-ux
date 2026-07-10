using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Linx.Data;

namespace Linx.Business.Common
{
    public class MethodsEntitySearch
    {
        public static EntitySearch ExcludedPropertiesEntitySearch(EntitySearch entitySearch, List<string> propriedadesExcluir)
        {
            for (int i = 0; i < entitySearch.Expressions.Count() - 1; i++)
            {
                if (entitySearch.Expressions[0].Name == "Field")
                {
                    var propriedade = entitySearch.Expressions[i].Value.ToString();
                    if (propriedadesExcluir.Contains(propriedade))
                    {
                        entitySearch.Expressions[i].Excluded = true;
                        entitySearch.Expressions[i + 1].Excluded = true;
                        entitySearch.Expressions[i + 2].Excluded = true;
                        if (entitySearch.Expressions.Count > (i + 3))
                            entitySearch.Expressions[i + 3].Excluded = true;
                    }
                }
            }

            var expressoes = entitySearch.Expressions.Where(x => x.Excluded == false);
            if (expressoes.Count() > 0)
            {
                if (expressoes.First().Name == "Condition") expressoes.First().Excluded = true;
                if (expressoes.Last().Name == "Condition") expressoes.Last().Excluded = true;
            }

            return entitySearch;
        }

        /// <summary>
        /// Converte o valor para o tipoDado
        /// </summary>
        /// <param name="entitySearch"></param>
        /// <param name="propertyName">propriedade para encontrar no entitySearch</param>
        /// <param name="tipoDado">tipo de dado para retornar</param>
        /// <returns>Retornará o valor convertido para o tipo de dados passado ou nulo se não houver propriedade para converter</returns>
        public static dynamic GetValueEntitySearch(EntitySearch entitySearch, string propertyName, Type tipoDado)
        {
            if (entitySearch.GetExpressionValue(propertyName) != null)
            {
                var valor = entitySearch.GetExpressionValue(propertyName).ToString();
                try
                {
                    return Convert.ChangeType(valor, tipoDado);
                }
                catch (Exception ex)
                {
                    throw new Exception("Problema para converter para " + tipoDado.GetTypeInfo().Name + ". Erro: " + ex.Message.ToString());
                }
            }
            return null;
        }

        public static RetornoDynQueryEntitySearch GetDynQueryEntitySearch(EntitySearch entitySearch, List<string> propriedadesExcluir, Type entityType)
        {
            if (entitySearch == null) return new RetornoDynQueryEntitySearch() { dynQuery = "true", entitySearch = entitySearch, parameters = new List<ObjectParameter>() };

            if (propriedadesExcluir == null) propriedadesExcluir = new List<string>();
            if (propriedadesExcluir.Count > 0)
                entitySearch = ExcludedPropertiesEntitySearch(entitySearch, propriedadesExcluir);
            
            string dynQuery = String.Empty;
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            if (entitySearch.Expressions.Count > 0)
            {
                List<EntitySearch> entitySearchList = new List<EntitySearch>();
                entitySearchList.Add(entitySearch);
                List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, entityType);
                replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
            }

            if (dynQuery.IsNullOrEmpty())
                dynQuery = "true";

            return new RetornoDynQueryEntitySearch
            {
                dynQuery = dynQuery,
                parameters = parameters,
                entitySearch = entitySearch
            };
        }

        public static RetornoDynQueryEntitySearch GetDynQueryEntitySearch(List<EntitySearch> entitySearchList, List<string> propriedadesExcluir, Type entityType)
        {
            if (entitySearchList == null) return new RetornoDynQueryEntitySearch() { dynQuery = "true", parameters = new List<ObjectParameter>(), entitySearchList = entitySearchList };

            if (propriedadesExcluir == null) propriedadesExcluir = new List<string>();
            if (propriedadesExcluir.Count > 0)
            {
                foreach (var entitySearch in entitySearchList)
                {
                    ExcludedPropertiesEntitySearch(entitySearch, propriedadesExcluir);
                }
            }

            string dynQuery = String.Empty;
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, entityType);
            replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
           
            if (dynQuery.IsNullOrEmpty())
                dynQuery = "true";

            return new RetornoDynQueryEntitySearch
            {
                dynQuery = dynQuery,
                parameters = parameters,
                entitySearchList = entitySearchList
            };
        }
    }


    public class RetornoDynQueryEntitySearch
    {
        public string dynQuery { get; set; }
        private List<ObjectParameter> _parameters = new List<ObjectParameter>();
        public List<ObjectParameter> parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }
        public EntitySearch entitySearch { get; set; }

        private List<EntitySearch> _entitySearchList;

        public List<EntitySearch> entitySearchList
        {
            get { return _entitySearchList; }
            set { _entitySearchList = value; }
        }

    }
}
