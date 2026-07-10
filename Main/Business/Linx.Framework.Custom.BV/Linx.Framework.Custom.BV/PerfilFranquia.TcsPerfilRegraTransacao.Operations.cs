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
using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.Custom.BV.PerfilFranquia
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsPerfilRegraTransacao
    {
        public string GetDescTransacao()
        {
            if (!this._DescTransacao.IsNullOrEmpty() || this.IdTransacao.IsNullOrEmpty())
                return this._DescTransacao;

            return Framework.BV.Utils.GetDescTransacao(this.IdTransacao).Extract("[", "]");
        }

        public string GetOrigem()
        {
            if (!this._Origem.IsNullOrEmpty() || this.IdTransacao.IsNullOrEmpty())
                return this._Origem;

            return Framework.BV.Utils.GetDescTransacao(this.IdTransacao).Extract(",[", "]", 3);
        }
    }
}
