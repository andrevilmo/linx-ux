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

namespace Linx.Framework.BV.Modulo
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsModuloDoGrupoDetalhe
    {
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsModuloDoGrupoDetalhe(ref IQueryable<LookUpTcsModuloDoGrupoDetalhe> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition =
            from result in Utils.GetLookUpModulo(entitySearch)
            select new LookUpTcsModuloDoGrupoDetalhe()
            {
                IdModulo = result.IdModulo,
                DescModulo = result.DescModulo
            };
        }
    }
}
