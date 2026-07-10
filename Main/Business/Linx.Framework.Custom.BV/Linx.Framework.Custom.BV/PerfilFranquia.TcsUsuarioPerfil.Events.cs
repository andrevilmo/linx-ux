using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Reflection;
using Linx.Framework.ControleSistema.BM;
using Linx.Business.Tools;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.Custom.BV.PerfilFranquia
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioPerfil
    {
        public static void OnLookingUpLookUpTcsUsuario(ref IQueryable<LookUpTcsUsuario> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Int32 idGpecon = UserServiceHelper.GetCurrentIdGpecon().GetValueOrDefault();
            searchDefinition = searchDefinition.Where(i => i.IdLinx == idGpecon);
        }

        public static void OnSavedContextChanges(PerfilFranquiaDomainService context, ChangeSetEntry[] entities)
        {
            entities.Where(i => i.Entity is TcsUsuarioPerfil && i.Operation != DomainOperation.None).Select(i => ((TcsUsuarioPerfil)i.Entity).UidUsuario).Distinct().ToList().ForEach(uidUsuario =>
            {
                if (entities.Where(i => i.Entity is TcsUsuarioPerfil && i.Operation != DomainOperation.None).Count() > 0)
                {
                    //Remove user modules && BandeiraRede from cache
                    int idAmbiente = UserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault();
                    Framework.BV.Utils.RemoveUserModulesFromCache(uidUsuario, idAmbiente);
                    Framework.BV.Utils.RemoveUserBandeiraRedeFromCache(uidUsuario, idAmbiente);
                    Framework.BV.Utils.RemoveBrandInfoFromCache(uidUsuario);
                }
            });
        }
    }
}
