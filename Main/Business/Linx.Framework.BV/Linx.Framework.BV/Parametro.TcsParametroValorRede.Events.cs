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
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.Framework.BV.Parametro
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsParametroValorRede
    {
        /// Execute before lookup on server side.
        public static void OnLookingUpLookUpParametroRede(ref IQueryable<LookUpParametroRede> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Parametro.ParametroDomainService ds = new ParametroDomainService();

            entitySearch.EntityName = string.Empty;

            searchDefinition = (
                from result in ds.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                select new LookUpParametroRede
                {
                    IdBandeiraRedeParam = result.IdBandeiraRedeParam,
                    CodBandeiraRede = result.CodBandeiraRede,
                    DescBandeiraRede = result.DescBandeiraRede,
                    ChaveSelecao = result.IdBandeiraRedeParam.ToString(),
                    IdBandeiraRedeString = result.IdBandeiraRedeParam.ToString()
                });
        }
    }
}
