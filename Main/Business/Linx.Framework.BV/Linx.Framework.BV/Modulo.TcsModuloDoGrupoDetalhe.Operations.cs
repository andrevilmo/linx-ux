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
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;

namespace Linx.Framework.BV.Modulo
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsModuloDoGrupoDetalhe
	{
        public string GetDescModulo()
        {
            if (!this._DescModulo.IsNullOrEmpty() || this.IdModulo.IsNullOrEmpty())
                return this._DescModulo;

            string descriptions = Utils.GetDescModulo(this.IdModulo);
            this._DescModulo = descriptions.Extract("[", "]");
            return this._DescModulo;
        }
    }
}
