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

namespace Linx.TCS0101.BO.TcsObjeto
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsLayout
	{
        /// Occurs after add a new element.
        public void OnAdded()
        {
            this.UidLayout = this.UidObjetoConteudo;
            this.Idioma = System.Globalization.CultureInfo.CurrentCulture.Name;
#if SILVERLIGHT
            this.UidUsuario = System.Windows.Application.Current.GetCurrentUserId();
            this.NomeUsuario = System.Windows.Application.Current.GetCurrentUserName();
#endif

        }

        /// Occurs after the value changed.
        partial void OnLayoutPadraoChanged()
        {
            if (this.TcsObjeto != null)
            {
                if ((bool)this.LayoutPadrao)
                {
                    foreach (TcsLayout layout in this.TcsObjeto.TcsLayoutList)
                    {
                        if (layout != this)
                        {
                            layout.LayoutPadrao = false;
                        }
                    }
                }
            }
        }
    }
}
