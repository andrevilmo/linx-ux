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
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV.Parametro
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsParametroTabelaSelecao
    {
        /// Execute before lookup on server side.
        //public static void OnLookUpingLookupTcsTabelaAutorizacao(ref IQueryable<LookupTcsTabelaAutorizacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        //{
        //    entitySearch.EntityName = string.Empty;
        //    AutorizacaoDomainService ds = new AutorizacaoDomainService();
        //    searchDefinition =
        //        (from result in ds.GetTcsTabelaAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
        //         select new LookupTcsTabelaAutorizacao
        //         {
        //             UidTabela = result.UidTabela,
        //             DescTabela = result.DescTabela,
        //             NomeTabela = result.NomeTabela
        //         });
        //}
    }
}
