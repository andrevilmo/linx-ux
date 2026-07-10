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

namespace Linx.Framework.BV.Usuario
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioFilial
    {
        public static void OnSavedContextChanges(UsuarioDomainService context, ChangeSetEntry[] entities)
        {
            entities.Where(i => i.Entity is TcsUsuarioFilial && i.Operation != DomainOperation.None).Select(i => ((TcsUsuarioFilial)i.Entity).TcsUsuario.UidUsuario).Distinct().ToList().ForEach(uidUsuario =>
            {
                Utils.RemoveBrandInfoFromCache(uidUsuario);
            });
        }
    }
}
