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
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Xml.Serialization;

namespace Linx.TCS0101.BO.TcsPerfil
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsFiltroExpressaoView
	{
        /// Occurs after add a new element.
        public void OnAdded()
        {
            if (this.TcsPerfilTransacaoFiltro != null && this.TcsPerfilTransacaoFiltro.TcsFiltroExpressaoViewList.Count() > 1)
            {
                this.Id = this.TcsPerfilTransacaoFiltro.TcsFiltroExpressaoViewList.Where(e => e != this).Max(e => e.Id) + 1;
            }
            else this.Id = 1;
        }
    }
}
