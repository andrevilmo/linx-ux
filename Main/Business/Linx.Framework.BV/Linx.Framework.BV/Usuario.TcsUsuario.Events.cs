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

namespace Linx.Framework.BV.Usuario
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuario
    {
        /// Execute after save context changes.
        public static void OnSavedContextChanges(UsuarioDomainService context, ChangeSetEntry[] entities)
        {
            entities.Where(i => i.Entity is TcsUsuario && i.Operation != DomainOperation.None).Select(i => ((TcsUsuario)i.Entity).UidUsuario).Distinct().ToList().ForEach(uidUsuario =>
            {
                int idAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault();
                Utils.RemoveUserModulesFromCache(uidUsuario, idAmbiente);
                Utils.RemoveUserBandeiraRedeFromCache(uidUsuario, idAmbiente);
            });
        }

        /// Execute before search data.
        public static void OnSearching(ref IQueryable<TcsUsuario> searchDefinition, bool noAssociations, List<EntitySearch> searchList)
        {
            List<int> lstIdGpecon = Utils.GetCompanyGpeconList();
            searchDefinition = searchDefinition.Where(i => lstIdGpecon.Contains(i.IdLinx));
        }
    }
}
