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

namespace Linx.Framework.BV.Usuario
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsFiltroExpressaoView
    {
        /// Occurs after add a new element.
        //public void OnAdded()
        //{
        //    if (this.TcsUsuarioTransacaoFiltro != null && this.TcsUsuarioTransacaoFiltro.TcsFiltroExpressaoViewList.Count() > 1)
        //    {
        //        this.Id = this.TcsUsuarioTransacaoFiltro.TcsFiltroExpressaoViewList.Where(e => e != this).Max(e => e.Id) + 1;
        //    }
        //    else this.Id = 1;
        //}
    }
}
