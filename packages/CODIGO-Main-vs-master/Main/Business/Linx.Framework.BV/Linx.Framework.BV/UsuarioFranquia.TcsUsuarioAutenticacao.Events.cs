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

namespace Linx.Framework.BV.UsuarioFranquia
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAutenticacao
    {
        /// Execute before search data.
        /// Execute before search data.
        public static void OnSearching(ref IQueryable<TcsUsuarioAutenticacao> searchDefinition, bool noAssociations, List<EntitySearch> searchList)
        {
            // Align with UsuarioAutorizacao export gpecon scope (EconomicGroup header).
            // Note: EntitySearch paths also apply ApplyCurrentGpeconFilter in Autorizacao (includes multi-gpecon);
            // this keeps GetTcsUsuarioAutenticacao / NoAssociations scoped when Autorizacao has not filtered yet.
            int idGpecon = BusinessUserServiceHelper.GetCurrentIdGpecon().GetValueOrDefault();
            if (idGpecon > 0)
                searchDefinition = searchDefinition.Where(i => i.IdLinx == idGpecon);
        }

        /// Execute before save changes.
        public void OnSavingChanges(UsuarioFranquiaDomainService context, ChangeOperation changeOperation)
        {
            if (changeOperation == ChangeOperation.Insert)
            {
                //IdLinx = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment().GetValueOrDefault();
                IdLinx = BusinessUserServiceHelper.GetCurrentIdGpecon().GetValueOrDefault();

                if (this.UidUsuario.IsNullOrEmpty())
                {
                    this.UidUsuario = Guid.NewGuid();
                }
            }

        }
    }
}
