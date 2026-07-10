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
using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.BV.TransacaoAutorizacao
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsTransacaoMenuAutorizacao
    {
        public static void OnLookingUpLookUpTcsModuloMenuAutorizacao(ref IQueryable<LookUpTcsModuloMenuAutorizacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {

        }
    }
}
