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
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using Linx.Framework.Autorizacao.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.Framework.BV.UsuarioAutorizacao
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAutenticacaoAcessoP
    {
        public static void OnLookingUpLookUpTcsAmbiente2(ref IQueryable<LookUpTcsAmbiente2> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            if (entitySearch == null)
                entitySearch = new EntitySearch();

            Int64 userId = ResolveLookUpTcsAmbiente2UserId(entitySearch);
            int idLinx = ResolveLookUpTcsAmbiente2LoggedIdLinx();
            AddLookUpTcsAmbiente2IdLinxFilter(entitySearch, idLinx);

            List<int> assignedEnvironments = new List<int>();
            if (userId != 0)
            {
                UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
                EntitySearch userFilter = new EntitySearch();
                userFilter.EntityName = string.Empty;
                userFilter.Expressions.Add(new EntitySearchExpression("Field", "IdUsuario"));
                userFilter.Expressions.Add(new EntitySearchExpression("Operator", "=="));
                userFilter.Expressions.Add(new EntitySearchExpression("Value", userId));

                List<EntitySearch> assignedSearch = new List<EntitySearch>();
                assignedSearch.Add(userFilter);

                foreach (TcsUsuarioAutenticacaoAcessoP row in ds.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(assignedSearch)))
                {
                    if (!assignedEnvironments.Contains(row.IdTcsAmbiente))
                        assignedEnvironments.Add(row.IdTcsAmbiente);
                }
            }

            Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();
            entitySearch.EntityName = string.Empty;
            List<EntitySearch> environmentSearch = new List<EntitySearch>();
            environmentSearch.Add(entitySearch);

            List<LookUpTcsAmbiente2> result = new List<LookUpTcsAmbiente2>();
            foreach (Ambiente.TcsAmbiente env in dsAmbiente.GetTcsAmbienteByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(environmentSearch)))
            {
                if (idLinx != 0 && env.IdLinx != idLinx)
                    continue;

                if (assignedEnvironments.Contains(env.IdTcsAmbiente))
                    continue;

                LookUpTcsAmbiente2 item = new LookUpTcsAmbiente2();
                item.DescricaoAmbiente = env.DescricaoAmbiente;
                item.DescricaoAplicativo = env.DescricaoAplicativo;
                item.NomeEmpresa = env.NomeEmpresa;
                item.DescricaoAplicacao = env.DescricaoAplicacao;
                item.IdTcsAmbiente = env.IdTcsAmbiente;
                item.IdAplicacao = env.IdAplicacao;
                item.IdTcsAplicativo = env.IdTcsAplicativo;
                item.IdLinx = env.IdLinx;
                result.Add(item);
            }

            searchDefinition = result.AsQueryable();
        }

        /// <summary>
        /// Cadastro Usuario Local must list TCS_AMBIENTE rows for the user being edited,
        /// excluding environments already on that user's Ambientes tab.
        /// Implemented without lambdas so the method can be published onto an existing Service assembly.
        /// </summary>
        public static Int64 ResolveLookUpTcsAmbiente2UserId(EntitySearch entitySearch)
        {
            if (entitySearch == null || entitySearch.Expressions == null)
                return 0;

            entitySearch.EntityName = string.Empty;

            EntitySearchExpression expression = null;
            int fieldPos = -1;
            for (int i = 0; i < entitySearch.Expressions.Count; i++)
            {
                EntitySearchExpression candidate = entitySearch.Expressions[i];
                if (candidate != null
                    && candidate.Name == "Field"
                    && candidate.Value != null
                    && candidate.Value.ToString() == "IdUsuario")
                {
                    expression = candidate;
                    fieldPos = i;
                    break;
                }
            }

            if (expression.IsNull() || fieldPos < 0)
                return 0;

            if ((fieldPos + 2) < entitySearch.Expressions.Count
                && entitySearch.Expressions[fieldPos + 2].Value != null)
            {
                Int64 userId = Convert.ToInt64(entitySearch.Expressions[fieldPos + 2].Value.ToString());
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
                return userId;
            }

            return 0;
        }

        /// <summary>
        /// Cadastro Usuario Local list and Ambientes lookup must only show TCS_AMBIENTE
        /// rows whose IdLinx matches the logged-in company (CurrentCompany / EconomicGroup).
        /// </summary>
        public static int ResolveLookUpTcsAmbiente2LoggedIdLinx()
        {
            int idLinx = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment().GetValueOrDefault();
            if (idLinx != 0)
                return idLinx;

            return BusinessUserServiceHelper.GetCurrentIdGpecon().GetValueOrDefault();
        }

        public static bool MatchesLookUpTcsAmbiente2LoggedIdLinx(int environmentIdLinx, int loggedIdLinx)
        {
            if (loggedIdLinx == 0)
                return true;

            return environmentIdLinx == loggedIdLinx;
        }

        public static void AddLookUpTcsAmbiente2IdLinxFilter(EntitySearch entitySearch, int idLinx)
        {
            if (entitySearch == null || idLinx == 0)
                return;

            if (entitySearch.Expressions == null)
                entitySearch.Expressions = new List<EntitySearchExpression>();

            for (int i = 0; i < entitySearch.Expressions.Count; i++)
            {
                EntitySearchExpression candidate = entitySearch.Expressions[i];
                if (candidate != null
                    && candidate.Name == "Field"
                    && candidate.Value != null
                    && candidate.Value.ToString() == "IdLinx")
                {
                    return;
                }
            }

            if (entitySearch.Expressions.Count != 0)
                return;

            entitySearch.Expressions.Add(new EntitySearchExpression("Field", "IdLinx"));
            entitySearch.Expressions.Add(new EntitySearchExpression("Operator", "=="));
            entitySearch.Expressions.Add(new EntitySearchExpression("Value", idLinx));
        }

        public static List<int> GetUnassignedLookUpTcsAmbiente2Ids(IEnumerable<int> allEnvironmentIds, IEnumerable<int> assignedEnvironmentIds)
        {
            List<int> unassigned = new List<int>();
            List<int> assigned = new List<int>();
            if (assignedEnvironmentIds != null)
            {
                foreach (int id in assignedEnvironmentIds)
                    assigned.Add(id);
            }

            if (allEnvironmentIds == null)
                return unassigned;

            foreach (int id in allEnvironmentIds)
            {
                if (!assigned.Contains(id))
                    unassigned.Add(id);
            }

            return unassigned;
        }

        public static void OnLookingUpLookUpTcsAmbiente2Relacionado(ref IQueryable<LookUpTcsAmbiente2Relacionado> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            searchDefinition = ds.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch })).Where(i => i.IdTcsAplicativo == 2).
                Select(i => new LookUpTcsAmbiente2Relacionado
            {
                DescricaoAmbienteRelacionado = i.DescricaoAmbiente,
                DescricaoAplicativo = i.DescricaoAplicativo,
                DescricaoAplicacao = i.DescricaoAplicacao,
                IdTcsAmbienteRelacionado = i.IdTcsAmbiente,
            }).Distinct();

        }
    }
}
