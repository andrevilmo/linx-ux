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
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.BV.Parametro
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsParametroValor
    {
        public void OnSavedChanges(ParametroDomainService context, ChangeOperation changeOperation)
        {
            if (TcsParametro.TituloParametro.ToUpper().StartsWith("AUDITORIA_"))
            {
                int idLinx = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment().GetValueOrDefault();
                WebCacheHelper.RemoveWebCache(string.Format("{0}_ID_LINX_{1}", TcsParametro.TituloParametro, idLinx));
            }
        }
    }
}
