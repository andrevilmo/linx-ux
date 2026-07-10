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
    public partial class TcsPerfilBandeiraRede
    {
        public static void OnLookingUpLookUpTbcBandeiraRede(ref IQueryable<LookUpTbcBandeiraRede> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            string serializedString = SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch });
            Linx.Framework.BV.Rede.RedeDomainService dsRede = new Framework.BV.Rede.RedeDomainService();
            searchDefinition = dsRede.GetTbcBandeiraRedeByEntitySearchNoAssociations(serializedString).Select(i => new LookUpTbcBandeiraRede { IdBandeiraR = i.IdBandeiraRede, DescBandeiraRede = i.DescBandeiraRede }).AsQueryable();
        }

        public static void OnSavedContextChanges(PerfilFranquiaDomainService context, ChangeSetEntry[] entities)
        {
            if (entities.Where(i => i.Entity is TcsPerfilBandeiraRede && i.Operation != DomainOperation.None).Count() > 0)
            {
                Linx.Framework.BV.Utils.RemoveBandeiraRedeFromCache();
            }
        }
    }
}
