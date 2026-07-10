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

namespace Linx.Framework.Custom.BV.PerfilFranquia
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsPerfilFilial
    {
        public static void OnSavedContextChanges(PerfilFranquiaDomainService context, ChangeSetEntry[] entities)
        {
            var entity = entities.Where(i => i.Entity is TcsPerfilFilial && i.Operation != DomainOperation.None).FirstOrDefault();

            if (!entity.IsNullOrEmpty())
            {
                long idPerfil = (entity.Entity as TcsPerfilFilial).IdPerfil;
                context.GetTcsUsuarioPerfilNoAssociations().Where(predicate: i => i.IdPerfil == idPerfil).Select(i => i.UidUsuario).ToList().ForEach(uidUsuario =>
                {
                    Linx.Framework.BV.Utils.RemoveBrandInfoFromCache(uidUsuario);
                });
            }
        }

        public static void OnLookingUpLookUpTbcFilial(ref IQueryable<LookUpTbcFilial> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            string serializedString = SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch });
            PerfilFranquiaDomainService ds = new PerfilFranquiaDomainService();
            searchDefinition = ds.GetTbcFilialByEntitySearchNoAssociations(serializedString).Select(i => new LookUpTbcFilial { IdFilialPfj = i.IdFilialPfj, CodigoFilial = i.CodigoFilial, NomeFilial = i.NomeFilial }).AsQueryable();
        }
    }
}
