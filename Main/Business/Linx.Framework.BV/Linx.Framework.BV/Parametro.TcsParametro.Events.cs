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
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx.Framework.BV.ParametroAutorizacao;

namespace Linx.Framework.BV.Parametro
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsParametro
    {
        /// Execute before lookup on server side.
        public static void OnLookingUpLookUpTcsParametroGrupoAutorizacao(ref IQueryable<LookUpTcsParametroGrupoAutorizacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            ParametroAutorizacaoDomainService ds = new ParametroAutorizacaoDomainService();
            entitySearch.EntityName = string.Empty;

            searchDefinition = (from result in ds.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                select new LookUpTcsParametroGrupoAutorizacao() {  IdGrupoParametro = result.IdGrupoParametro, DescGrupoParametro = result.DescGrupoParametro});
        }

        /// Execute before search data.
        public static void OnSearching(ref IQueryable<TcsParametro> searchDefinition, bool noAssociations, List<EntitySearch> searchList)
        {
            Dictionary<string, string> variacao = new Dictionary<string, string>();
            variacao.Add("TCS_USUARIO", BusinessUserServiceHelper.GetCurrentUserUid().ToString());
            var parameterValue = LinxBusinessParameters.GetParameter<string>("NIVEL_ACESSO_PARAMETRO", variacao);
            

            if (parameterValue.IsNullOrEmpty())
            {
                throw new Exception("Não foi encontrado valor para o parâmetro 'NIVEL_ACESSO_PARAMETRO'.");
            }

            Int64 nivelAcesso = Convert.ToInt64(parameterValue);
            int idTcsAplicativo = BusinessUserServiceHelper.GetCurrentApplicativeId().GetValueOrDefault();
            searchDefinition = searchDefinition.Where(i => i.NivelAcesso >= nivelAcesso && (i.IdTcsAplicativo == idTcsAplicativo || i.IdTcsAplicativo == 1));
        }

        public static void OnLookingUpLookUpTcsAplicativo(ref IQueryable<LookUpTcsAplicativo> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Aplicativo.AplicativoDomainService ds = new Aplicativo.AplicativoDomainService();
            entitySearch.EntityName = string.Empty;
            int idTcsAplicativo = BusinessUserServiceHelper.GetCurrentApplicativeId().GetValueOrDefault();
            searchDefinition = (from result in ds.GetTcsAplicativoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                select new LookUpTcsAplicativo() { IdTcsAplicativo = result.IdTcsAplicativo, DescricaoAplicativo = result.DescricaoAplicativo }).Where(i => i.IdTcsAplicativo == idTcsAplicativo || i.IdTcsAplicativo == 1);
        }
    }
}
