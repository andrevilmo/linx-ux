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

namespace Linx.Framework.BV.Perfil
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioPerfil
    {
        /// Execute after save context changes.
        public static void OnSavedContextChanges(PerfilDomainService context, ChangeSetEntry[] entities)
        {
            entities.Where(i => i.Entity is TcsUsuarioPerfil && i.Operation != DomainOperation.None).Select(i => ((TcsUsuarioPerfil)i.Entity).UidUsuario).Distinct().ToList().ForEach(uidUsuario =>
            {
                if (entities.Where(i => i.Entity is TcsUsuarioPerfil && i.Operation != DomainOperation.None).Count() > 0)
                {
                    //Remove user modules && BandeiraRede from cache
                    int idAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault();
                    Utils.RemoveUserModulesFromCache(uidUsuario, idAmbiente);
                    Utils.RemoveUserBandeiraRedeFromCache(uidUsuario, idAmbiente);
                    Utils.RemoveBrandInfoFromCache(uidUsuario);
                }
            });
        }

        public static void OnLookingUpLookUpTcsUsuario(ref IQueryable<LookUpTcsUsuario> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            List<int> lstIdGpecon = Utils.GetCompanyGpeconList();
            searchDefinition = searchDefinition.Where(i => lstIdGpecon.Contains(i.IdLinx));
        }
    }
}
