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
using System.Xml.Serialization;

namespace Linx.Framework.BV.IndicadorMedida
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsIndicadorIndice
    {
        /// Occurs after add a new element.
        public void OnAdded()
        {
            this.IndicaMedidaAlvo = true;
            this.Rgb = 0;
        }
    }
}
