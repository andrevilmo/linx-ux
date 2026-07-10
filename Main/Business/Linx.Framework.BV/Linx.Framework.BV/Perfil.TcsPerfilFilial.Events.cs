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
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.BV.Perfil
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsPerfilFilial
    {
        public static void OnSavedContextChanges(PerfilDomainService context, ChangeSetEntry[] entities)
        {
            var entity = entities.Where(i => i.Entity is TcsPerfilFilial && i.Operation != DomainOperation.None).FirstOrDefault();

            if (!entity.IsNullOrEmpty())
            {
                long idPerfil = (entity.Entity as TcsPerfilFilial).IdPerfil;
                context.GetTcsUsuarioPerfilNoAssociations().Where(predicate: i => i.IdPerfil == idPerfil).Select(i => i.UidUsuario).ToList().ForEach(uidUsuario =>
               {
                   Utils.RemoveBrandInfoFromCache(uidUsuario);
               });
            }
        }
    }
}
