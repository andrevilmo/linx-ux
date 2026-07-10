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

namespace Linx.Framework.BV.TransacaoTag
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsTransacaoTag
    {
        public static void OnLookingUpLookUpTcsTransacao(ref IQueryable<LookUpTcsTransacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition = (from result in Utils.GetLookupTransacao(entitySearch)
                                select new LookUpTcsTransacao()
                                {
                                    IdTransacao = result.IdTransacao,
                                    DescTransacao = result.DescTransacao,
                                    CodTransacao = result.CodTransacao
                                });
        }

        public static void OnSavedContextChanges(TransacaoTagDomainService context, ChangeSetEntry[] entities)
        {
            if (entities.Where(i => i.Entity is TcsTransacaoTag && i.Operation != DomainOperation.None).Count() > 0)
                Utils.RemoveModulesFromCache();
        }
    }
}
