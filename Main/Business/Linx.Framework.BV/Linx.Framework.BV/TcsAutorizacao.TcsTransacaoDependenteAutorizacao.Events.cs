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
using Linx.Framework.Autorizacao.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.TCS0101.BO.TcsAutorizacao
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsTransacaoDependenteAutorizacao
	{
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsTransacaoDependente(ref IQueryable<LookUpTcsTransacaoDependente> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            TcsAutorizacaoDomainService ds = new TcsAutorizacaoDomainService();
            entitySearch.EntityName = string.Empty;

            searchDefinition =
                (from result in ds.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                 select new LookUpTcsTransacaoDependente 
                 {
                     UidTransacao = result.UidTransacao,
                     DescTransacao = result.DescTransacao,
                     ClasseNome = result.ClasseNome
                 });
        }
    }
}
