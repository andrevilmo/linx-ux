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

namespace Linx.Framework.BV.TransacaoAutorizacao
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsTransacaoDependenteAutorizacao
    {
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsTransacaoDependente(ref IQueryable<Linx.Framework.BV.TransacaoAutorizacao.LookUpTcsTransacaoDependente> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            TransacaoAutorizacaoDomainService ds = new TransacaoAutorizacaoDomainService();
            entitySearch.EntityName = string.Empty;

            searchDefinition =
                (from result in ds.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                 select new LookUpTcsTransacaoDependente
                 {
                     IdTransacao = result.IdTransacao,
                     DescTransacao = result.DescTransacao,
                     ClasseNome = result.ClasseNome
                 });
        }
    }
}
